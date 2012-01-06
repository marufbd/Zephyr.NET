#region CODE HISTORY
/* -------------------------------------------------------------------------------- 
 * Client Name: 
 * Project Name: MyFrameWork.Domain
 * Module: 
 * Name: DomainEntity
 * Purpose:              
 * Author: latifur.rahman
 * Language: C# SDK version 3.5
 * --------------------------------------------------------------------------------
 * Change History:
 *  version: 1.0    latifur.rahman  1/2/2012 9:50:30 AM
 *  Description: Development Starts
 * -------------------------------------------------------------------------------- */
#endregion CODE HISTORY

#region REFERENCES
using System;

#endregion REFERENCES

namespace MyFrameWork.Domain
{
    public abstract class DomainEntity
    {        
        public virtual long Id { get; set; }

        public virtual string CreatedBy { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual  string LastUpdatedBy { get; set; }
        public virtual DateTime LastUpdatedAt { get; set; }

        protected DomainEntity()
        {
            this.CreatedAt = DateTime.Now;
            this.LastUpdatedAt = DateTime.Now;
        }
    }
}