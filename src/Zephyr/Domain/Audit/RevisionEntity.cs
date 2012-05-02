using System;

namespace Zephyr.Domain.Audit
{
    public class RevisionEntity : Entity, IRevisionEntity
    {
        //[RevisionNumber]
        public virtual long RevNo { get; set; }

        //[RevisionTimestamp]
        public virtual DateTime RevisionTimestamp { get; set; }

        public virtual string RevisionBy { get; set; }

        public override bool Equals(object obj)
        {
            var casted = obj as RevisionEntity;
            if (casted == null)
                return false;
            return (Id == casted.Id &&
                    RevisionTimestamp.Equals(casted.RevisionTimestamp) &&
                    RevisionBy.Equals(casted.RevisionBy));
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ RevisionTimestamp.GetHashCode() ^ RevisionBy.GetHashCode();
        }
    }
}