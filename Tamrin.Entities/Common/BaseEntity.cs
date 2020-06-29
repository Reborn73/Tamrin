using System;
using System.Collections.Generic;
using System.Text;

namespace Tamrin.Entities.Common
{
    public abstract class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }
    }
}
