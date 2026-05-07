using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Baytak.Application.DTOs;
using Baytak.Application.DTOs.Auth;
using Baytak.Application.Interfaces;
using Baytak.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace Baytak.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, IEmailService emailService)
        {
            _userManager = userManager;
            _config = config;
            _emailService = emailService;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                if (!existingUser.EmailConfirmed)
                {

                    var resendOtp = await _userManager.GenerateTwoFactorTokenAsync(existingUser, "Email");

                    await _emailService.SendEmailAsync(
                        existingUser.Email,
                        "Confirm Your Email",
                        $"Your OTP code is: {resendOtp}"
                    );

                    throw new Exception("Account exists but not confirmed. OTP resent.");
                }

                throw new Exception("User already exists");
            }

            var user=new ApplicationUser
            {
                FullName=dto.FullName,
                Email=dto.Email,
                UserName=dto.Email
            };

            var result=await _userManager.CreateAsync(user,dto.Password);
            if(!result.Succeeded)
            {
                var errors=string.Join(", ",result.Errors.Select(e=>e.Description));
                throw new Exception($"User registration failed: {errors}");
            }

            
            var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");
            await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", $"Your OTP code is: {otp}");

            user.LastOtpSentAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);

            return new AuthResponseDto
            {
                Token= null,
                Email=user.Email
            };
        }


        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user=await _userManager.FindByEmailAsync(dto.Email);

            if(user==null || !await _userManager.CheckPasswordAsync(user,dto.Password))
            {
                throw new Exception("Invalid email or password");
            }

            if (!user.EmailConfirmed)
                throw new Exception("Email not confirmed. Please check your OTP or request a new one.");

            var token= GenerateToken(user);
            return new AuthResponseDto
            {
                Token=token,
                Email=user.Email
            };
        }

        private string GenerateToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id),
                new Claim (ClaimTypes.Email, user.Email),
            };

            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["jwt:Key"]));
            var creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["jwt:Issuer"],
                audience: _config["jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task ConfirmEmail(ConfirmEmailDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("User not found");

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", dto.OTP);

            if (isValid)
            {
                user.EmailConfirmed = true;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Invalid OTP code");
            }
        }

        public async Task ForgotPassword(string email)
        {
            var user =await _userManager.FindByEmailAsync(email);
            if (user == null) return;

            var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            await _emailService.SendEmailAsync(email, "Reset Password OTP", $"Your reset code is: {otp}");

        }

        public async Task ResetPassword(ResetPasswordDto dto)
        {
            var user=await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, "Email", dto.OTP);

            if (!isValid)
            {
                throw new Exception("Invalid or expired OTP code");
            }

            var removeResult = await _userManager.RemovePasswordAsync(user);
            if (!removeResult.Succeeded)
                throw new Exception("Error resetting password");

            var addResult = await _userManager.AddPasswordAsync(user, dto.NewPassword);
            if (!addResult.Succeeded)
            {
                var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                throw new Exception(errors);
            }
            await _userManager.UpdateSecurityStampAsync(user);
        }
        public async Task ResendOtpAsync(ResendOtpDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
                throw new Exception("User not found");

            if (user.EmailConfirmed)
                throw new Exception("User already confirmed");

            if (user.LastOtpSentAt.HasValue && (DateTime.UtcNow - user.LastOtpSentAt.Value).TotalSeconds < 30)
            {
                throw new Exception("Please wait before requesting another OTP");
            }

            // Generate new OTP
            var otp = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

            await _emailService.SendEmailAsync(
                user.Email,
                "Confirm Your Email",
                $"Your OTP code is: {otp}"
            );
            user.LastOtpSentAt = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
        }
    }
}
