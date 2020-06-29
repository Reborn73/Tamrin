using System;
using System.Collections.Generic;
using System.Text;

namespace Tamrin.Entities.Common
{
    public abstract class AuditEntity : IAuditEntity
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}
