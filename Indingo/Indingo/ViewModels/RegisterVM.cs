using System.ComponentModel.DataAnnotations;

namespace Indingo.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Name { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
        [RegularExpression("^[a-zA-Z]+$")]
        public string Surname { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(3)]
       
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
