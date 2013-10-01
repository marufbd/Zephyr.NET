using System;
using Zephyr.Domain.Audit;
using Zephyr.Initialization;

namespace Zephyr.Domain
{
    public class DomainEntity : EntityWithTypedId<Guid>, IAuditable
    {
        protected DomainEntity()
        {            
            
        }        

        public virtual bool IsNew { get { return this.Id == Guid.Empty; } }        

        #region Implementation of IAuditable

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string LastUpdatedBy { get; set; }
        public virtual DateTime LastUpdatedAt { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public virtual bool IsDeleted { get; set; }

        #endregion
    }    
}