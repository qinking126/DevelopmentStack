using System.ComponentModel.DataAnnotations;

namespace DevelopmentStack.Domain.Entities
{
    /// <summary>
    ///     This is a Name component
    /// </summary>
    public class Name
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(100, ErrorMessage = "First name must be 100 characters or fewer")]
        [Display(Name = "First Name")]
        public virtual string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Middle name must be 50 characters or fewer")]
        [Display(Name = "Middle Name")]
        public virtual string MiddleName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(100, ErrorMessage = "Last name must be 100 characters or fewer")]
        [Display(Name = "Last Name")]
        public virtual string LastName { get; set; }

        public virtual string FullName { get { return FirstName + " " + LastName; } }

        public Name()
        {
        }
    }
}
