using Baytak.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class Favorite: BaseEntity
    {
        public string UserId { get; set; }
        public Guid PropertyId { get; set; }

        public ApplicationUser User { get; set; }
        public Property Property { get; set; }
    }
}
