using Baytak.Application.DTOs;
using Baytak.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task ConfirmEmail(ConfirmEmailDto dto);
        Task ForgotPassword(string email);
        Task ResetPassword(ResetPasswordDto dto);
        Task ResendOtpAsync(ResendOtpDto dto);
    }
}
