using System.ComponentModel.DataAnnotations;

namespace Zephyr.Domain.Audit
{
    public class AuditChangeLog : DomainEntity
    {
        public virtual string EntityIdentifier { get; set; }
        
        public virtual string EntityType { get; set; }

        public virtual AuditType ActionType { get; set; }
        
        public virtual string ActionBy { get; set; }

        [Required]
        public virtual string PropertyName { get; set; }
        
        public virtual string OldPropertyValue { get; set; }

        public virtual string NewPropertyValue { get; set; }
    }
}