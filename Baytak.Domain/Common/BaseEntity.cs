using System;
using System.Collections.Generic;
using System.Text;

namespace Baytak.Domain.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
