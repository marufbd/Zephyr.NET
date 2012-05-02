using System;

namespace Zephyr.Domain.Audit
{
    public interface IRevisionEntity
    {        
        //[RevisionNumber]
        long Id { get; set; }

        //[RevisionTimestamp]
        DateTime RevisionTimestamp { get; set; }

        string RevisionBy { get; set; } 
    }
}