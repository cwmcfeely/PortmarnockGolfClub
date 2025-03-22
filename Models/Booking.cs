using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortmarnockGolfClub.Models
{
    /// <summary>
    /// Represents information about a player in a booking.
    /// </summary>
    public class PlayerInfo
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The handicap of the player.
        /// </summary>
        public int Handicap { get; set; }

        /// <summary>
        /// The membership number of the player (optional for guests).
        /// </summary>
        public int? MembershipNumber { get; set; } // Optional for guests
    }

    /// <summary>
    /// Represents a booking for a tee time at the golf club.
    /// </summary>
    public class Booking
    {
        /// <summary>
        /// The unique identifier for the booking.
        /// </summary>
        public int BookingID { get; set; }

        /// <summary>
        /// The membership number of the member who made the booking.
        /// </summary>
        [Required]
        public int MembershipNumber { get; set; }

        /// <summary>
        /// The date of the booking.
        /// </summary>
        [Required]
        public DateTime Date { get; set; }

        /// <summary>
        /// The time slot for the booking (e.g., "08:00", "08:15").
        /// </summary>
        [Required]
        public string TimeSlot { get; set; } = string.Empty;

        /// <summary>
        /// Serialized JSON string containing player details for the booking.
        /// </summary>
        [Required]
        public string PlayerDetails { get; set; } = string.Empty;

        /// <summary>
        /// A list of players associated with the booking.
        /// This property is not mapped to the database and is used to deserialize/serialize PlayerDetails.
        /// </summary>
        [NotMapped]
        public List<PlayerInfo> Players
        {
            // Deserialize PlayerDetails into a list of PlayerInfo objects
            get => string.IsNullOrEmpty(PlayerDetails)
                ? new List<PlayerInfo>() // Return an empty list if PlayerDetails is null or empty
                : JsonSerializer.Deserialize<List<PlayerInfo>>(PlayerDetails) ?? new List<PlayerInfo>();

            // Serialize a list of PlayerInfo objects into PlayerDetails
            set => PlayerDetails = JsonSerializer.Serialize(value);
        }
    }
}