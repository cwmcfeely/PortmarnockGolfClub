using System.ComponentModel.DataAnnotations;

namespace PortmarnockGolfClub.Models
{
    /// <summary>
    /// Represents a member of Portmarnock Golf Club
    /// </summary>
    public class Member
    {
        /// <summary>
        /// Unique identifier for the member (Primary Key)
        /// </summary>
        [Key]
        public int MembershipNumber { get; set; }

        /// <summary>
        /// Full name of the member
        /// </summary>
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Email address for member communications
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gender of the member (Male/Female/Other)
        /// </summary>
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Current handicap index (0-54)
        /// </summary>
        [Required(ErrorMessage = "Handicap is required")]
        [Range(0, 54, ErrorMessage = "Handicap must be between 0 and 54")]
        public int Handicap { get; set; }
    }
}