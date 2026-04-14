using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Application.DTOs.PropertyImage
{
    public class UploadImageDto
    {
        public Guid propertyId {  get; set; }
        public IFormFile file { get; set; }
    }
}
