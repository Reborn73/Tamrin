using System;

namespace Tamrin.Entities.Common
{
    public interface IBaseEntity
    {
        public long Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}