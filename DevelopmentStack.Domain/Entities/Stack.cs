using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SharpLite.Domain;
using SharpLite.Domain.Validators;

namespace DevelopmentStack.Domain.Entities
{
    [HasUniqueDomainSignature(ErrorMessage = "This stack already exists")]
    public class Stack : Entity
    {
        [DomainSignature]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Title must be 255 characters or fewer")]
        public virtual string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description must be 2000 characters or fewer")]
        public virtual string Description { get; set; }


        /// <summary>
        /// many-to-one from Stack to User
        /// </summary>
        public virtual User PostBy { get; set; }
     

        [Display(Name = "Post Date")]
        public virtual DateTime PostDate { get; set; }

      
        /// <summary>
        /// many-to-many from Stack to Tag
        /// </summary>
        public virtual IList<Tag> Tags { get; protected set; }
     


        public Stack()
        {
            Tags = new List<Tag>();
        }



    }
}
