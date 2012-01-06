using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;


namespace DevelopmentStack.Domain.Entities
{
    [HasUniqueDomainSignature(ErrorMessage="This email address already exists")]
    public class User : Entity
    {
        [DomainSignature]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email must be 255 characters or fewer")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(25, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 25 characters")]
        public virtual string Password { get; set; }

        public virtual DateTime CreateDate { get; set; }
        public virtual string Avatar { get; set; }
        public virtual byte AccountTypeId { get; set; }
        public virtual string OtherID { get; set; }

        /// <summary>
        ///     Address is a component.
        /// </summary>
        public virtual Name Name { get; set; }

        /// <summary>
        ///     one-to-many from User to Stack
        /// </summary>
        public virtual IList<Stack> Stacks { get; protected set; }

        /// <summary>
        ///     one-to-many from User to Tag
        /// </summary>
        public virtual IList<Tag> Tags { get; protected set; }

        /// <summary>
        ///     one-to-many from User to Favorite
        /// </summary>
        public virtual IList<Favorite> Favorites { get; protected set; }

        public User()
        {
            this.Stacks = new List<Stack>();
            this.Tags = new List<Tag>();
            this.Favorites = new List<Favorite>();
        }
    }
}
