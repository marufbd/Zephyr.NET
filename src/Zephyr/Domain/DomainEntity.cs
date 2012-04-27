using System;

namespace Zephyr.Domain
{
    public class DomainEntity : Entity
    {
        public virtual Guid Guid { get; set; }
    }
}