using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortmarnockGolfClub.Models
{
    public class PlayerInfo
    {
        public string Name { get; set; } = string.Empty;
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
        public string TimeSlot { get; set; } = string.Empty;

        [Required]
        public string PlayerDetails { get; set; } = string.Empty;

        [NotMapped]
        public List<PlayerInfo> Players
        {
            get => string.IsNullOrEmpty(PlayerDetails)
                ? new List<PlayerInfo>()
                : JsonSerializer.Deserialize<List<PlayerInfo>>(PlayerDetails) ?? new List<PlayerInfo>();

            set => PlayerDetails = JsonSerializer.Serialize(value);
        }
    }
}