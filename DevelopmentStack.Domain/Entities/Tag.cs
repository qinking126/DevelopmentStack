using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;


namespace DevelopmentStack.Domain.Entities
{
    public class Tag : Entity
    {
        [DomainSignature]
        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tag name must be between 2 and 50 characters")]
        public virtual string Name { get; set; }

        /// <summary>
        /// many-to-one from Tag to User
        /// </summary>
        public virtual User CreateBy { get; set; }

        /// <summary>
        /// many-to-many from Tag to Stack
        /// </summary>
        public virtual IList<Stack> Stacks { get; protected set; }

        public virtual DateTime CreateDate { get; set; }
    }
}
