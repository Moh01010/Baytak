using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.Auth
{
    public class ConfirmEmailDto
    {
        public string Email {  get; set; }
        public string OTP {  get; set; }
    }
}
