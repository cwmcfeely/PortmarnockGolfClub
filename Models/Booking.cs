using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortmarnockGolfClub.Models
{
    public class PlayerInfo
    {
        public string Name { get; set; }
        public int Handicap { get; set; }
        public int? MembershipNumber { get; set; } // Optional for guests
    }

    public class Booking
    {
        public int BookingID { get; set; }

        [Required]
        public int MembershipNumber { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string TimeSlot { get; set; } // Store as "HH:mm" format

        [Required]
        public string PlayerDetails { get; set; } // Stored as JSON

        [NotMapped]
        public List<PlayerInfo> Players
        {
            get => string.IsNullOrEmpty(PlayerDetails)
                ? new List<PlayerInfo>()
                : JsonSerializer.Deserialize<List<PlayerInfo>>(PlayerDetails);
            set => PlayerDetails = JsonSerializer.Serialize(value);
        }
    }
}