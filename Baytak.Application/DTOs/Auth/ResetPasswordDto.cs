using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Baytak.Application.DTOs.Auth
{
    public class ResetPasswordDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string OTP { get; set; }
        [Required,MinLength(8)]
        public string NewPassword { get; set; }
    }
}
