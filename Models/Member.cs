using System.ComponentModel.DataAnnotations;

namespace PortmarnockGolfClub.Models
{
    public class Member
    {
        [Key]
        public int MembershipNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Handicap is required")]
        [Range(0, 54, ErrorMessage = "Handicap must be between 0 and 54")]
        public int Handicap { get; set; }
    }
}
