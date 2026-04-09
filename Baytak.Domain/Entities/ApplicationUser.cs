using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Baytak.Domain.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName {  get; set; }

        public ICollection<Property> Properties { get; set; }

    }
}
