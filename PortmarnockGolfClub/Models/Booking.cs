namespace PortmarnockGolfClub.Models
{
    public class Booking
    {
        public int BookingID { get; set; }
        public int MembershipNumber { get; set; }
        public DateTime Date { get; set; }
        public string TimeSlot { get; set; }
        public string PlayerDetails { get; set; } // JSON or other format
    }
}
