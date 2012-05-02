using System;

namespace Zephyr.Domain.Audit
{
    /// <summary>
    /// Marker to log any property value change
    /// </summary>
    public interface IAuditable
    {
        //Audit Info
        string CreatedBy { get; set; }
        DateTime CreatedAt { get; set; }
        string LastUpdatedBy { get; set; }
        DateTime LastUpdatedAt { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        bool IsDeleted { get; set; }
    }
}