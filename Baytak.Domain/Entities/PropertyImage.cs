using Baytak.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Entities
{
    public class PropertyImage: BaseEntity
    {
        public string ImageUrl {  get; set; }

        public Guid PropertyId { get; set; }

        public Property Property { get; set; }
    }
}
