using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Baytak.Application.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        public string FullName {  get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required,MinLength(8)]
        public string Password { get; set; }
    }
}
