using System;
using SharpLite.Domain;


namespace DevelopmentStack.Domain.Entities
{
    public class Vote : Entity
    {
        /// <summary>
        /// many-to-one from Vote to User
        /// </summary>
        public virtual User VoteBy { get; set; }

        /// <summary>
        /// many-to-one from Vote to Stack
        /// </summary>
        public virtual Stack Stack { get; set; }

        public virtual DateTime VoteDate { get; set; }
        public virtual Byte Score { get; set; }

        public Vote()
        {
        }
    }
}
