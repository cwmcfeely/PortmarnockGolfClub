using Microsoft.EntityFrameworkCore;
using PortmarnockGolfClub.Data;
using PortmarnockGolfClub.Models;

namespace PortmarnockGolfClub.Services
{
    public class BookingService
    {
        private readonly GolfClubDbContext _context;

        public BookingService(GolfClubDbContext context)
        {
            _context = context;
        }

        // Generate available tee time slots for a day (15-min intervals)
        public List<string> GenerateTeeTimeSlots()
        {
            var slots = new List<string>();
            DateTime start = DateTime.Today.AddHours(8); // Starting at 8:00 AM
            DateTime end = DateTime.Today.AddHours(17); // Ending at 5:00 PM

            for (DateTime time = start; time <= end; time = time.AddMinutes(15))
            {
                slots.Add(time.ToString("HH:mm"));
            }

            return slots;
        }

        // Check if a member already has a booking for a specific date
        public async Task<bool> MemberHasBookingOnDate(int membershipNumber, DateTime date)
        {
            return await _context.Bookings
                .AnyAsync(b => b.MembershipNumber == membershipNumber &&
                               b.Date.Date == date.Date);
        }

        // Get available slots for a specific date
        public async Task<List<string>> GetAvailableSlotsAsync(DateTime date)
        {
            var allSlots = GenerateTeeTimeSlots();
            var bookedSlots = await _context.Bookings
                .Where(b => b.Date.Date == date.Date)
                .Select(b => b.TimeSlot)
                .ToListAsync();

            return allSlots.Except(bookedSlots).ToList();
        }

        // Create a new booking
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Validate: No more than 4 players
            if (booking.Players.Count > 4)
                throw new InvalidOperationException("Maximum 4 players allowed per tee time.");

            // Validate: Member can't book more than once per day
            if (await MemberHasBookingOnDate(booking.MembershipNumber, booking.Date))
                throw new InvalidOperationException("Members can only book one tee time per day.");

            // Validate: Valid 15-minute interval time slot
            if (!GenerateTeeTimeSlots().Contains(booking.TimeSlot))
                throw new InvalidOperationException("Invalid tee time slot.");

            // Add booking
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        // Get all bookings
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        // Get bookings for a date
        public async Task<List<Booking>> GetBookingsForDateAsync(DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.Date.Date == date.Date)
                .OrderBy(b => b.TimeSlot)
                .ToListAsync();
        }

        // Get booking by ID
        public async Task<Booking> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        // Update booking
        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            // Validate player count
            if (booking.Players.Count > 4)
                throw new InvalidOperationException("Maximum 4 players allowed per tee time.");

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Cancel booking
        public async Task CancelBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}