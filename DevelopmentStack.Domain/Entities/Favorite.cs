using System;
using SharpLite.Domain;


namespace DevelopmentStack.Domain.Entities
{
    public class Favorite : Entity
    {
        [DomainSignature]
        public virtual Stack Stack { get; set; }

        [DomainSignature]
        public virtual User User { get; set; }

        public virtual bool IsPublic { get; set; }
        public virtual DateTime CreateDate { get; set; }

        public Favorite()
        {
        }
    }
}
