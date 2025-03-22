using Microsoft.EntityFrameworkCore;
using PortmarnockGolfClub.Data;
using PortmarnockGolfClub.Models;

namespace PortmarnockGolfClub.Services
{
    public class BookingService
    {
        private readonly GolfClubDbContext _context;

        // Constructor injecting the database context
        public BookingService(GolfClubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Generates available tee time slots for a day in 15-minute intervals
        /// </summary>
        /// <returns>List of time slots in "HH:mm" format</returns>
        public List<string> GenerateTeeTimeSlots()
        {
            var slots = new List<string>();
            DateTime start = DateTime.Today.AddHours(8); // 8:00 AM start time
            DateTime end = DateTime.Today.AddHours(17);   // 5:00 PM end time

            // Generate 15-minute interval slots between start and end times
            for (DateTime time = start; time <= end; time = time.AddMinutes(15))
            {
                slots.Add(time.ToString("HH:mm"));
            }

            return slots;
        }

        /// <summary>
        /// Checks if a member has an existing booking on a specific date
        /// </summary>
        /// <param name="membershipNumber">Member's unique identifier</param>
        /// <param name="date">Date to check</param>
        /// <returns>True if member has existing booking, false otherwise</returns>
        public async Task<bool> MemberHasBookingOnDate(int membershipNumber, DateTime date)
        {
            return await _context.Bookings
                .AnyAsync(b => b.MembershipNumber == membershipNumber &&
                               b.Date.Date == date.Date);
        }

        /// <summary>
        /// Retrieves available time slots for a specific date
        /// </summary>
        /// <param name="date">Date to check availability</param>
        /// <returns>List of available time slots</returns>
        public async Task<List<string>> GetAvailableSlotsAsync(DateTime date)
        {
            var allSlots = GenerateTeeTimeSlots();
            var bookedSlots = await _context.Bookings
                .Where(b => b.Date.Date == date.Date)
                .Select(b => b.TimeSlot)
                .ToListAsync();

            // Return slots not present in booked slots
            return allSlots.Except(bookedSlots).ToList();
        }

        /// <summary>
        /// Creates a new booking with validation checks
        /// </summary>
        /// <param name="booking">Booking object to create</param>
        /// <returns>Created booking</returns>
        /// <exception cref="InvalidOperationException">Throws for validation failures</exception>
        public async Task<Booking> CreateBookingAsync(Booking booking)
        {
            // Validate player count
            if (booking.Players.Count > 4)
                throw new InvalidOperationException("Maximum 4 players allowed per tee time.");

            // Check for existing bookings
            if (await MemberHasBookingOnDate(booking.MembershipNumber, booking.Date))
                throw new InvalidOperationException("Members can only book one tee time per day.");

            // Validate time slot format
            if (!GenerateTeeTimeSlots().Contains(booking.TimeSlot))
                throw new InvalidOperationException("Invalid tee time slot.");

            // Add and save booking
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        /// <summary>
        /// Retrieves all bookings from the database
        /// </summary>
        public async Task<List<Booking>> GetAllBookingsAsync()
        {
            return await _context.Bookings.ToListAsync();
        }

        /// <summary>
        /// Gets bookings for a specific date
        /// </summary>
        /// <param name="date">Date to filter bookings</param>
        /// <returns>Sorted list of bookings for the date</returns>
        public async Task<List<Booking>> GetBookingsForDateAsync(DateTime date)
        {
            return await _context.Bookings
                .Where(b => b.Date.Date == date.Date)
                .OrderBy(b => b.TimeSlot)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves a booking by its ID
        /// </summary>
        /// <param name="id">Booking ID</param>
        /// <returns>Booking object or null if not found</returns>
        public async Task<Booking?> GetBookingByIdAsync(int id)
        {
            return await _context.Bookings.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing booking
        /// </summary>
        /// <param name="booking">Updated booking object</param>
        /// <returns>True if update successful</returns>
        /// <exception cref="InvalidOperationException">Throws for invalid player count</exception>
        public async Task<bool> UpdateBookingAsync(Booking booking)
        {
            // Validate player count before update
            if (booking.Players.Count > 4)
                throw new InvalidOperationException("Maximum 4 players allowed per tee time.");

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a booking by ID
        /// </summary>
        /// <param name="id">Booking ID to delete</param>
        public async Task DeleteBookingAsync(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking != null)
            {
                _context.Bookings.Remove(booking);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Gets all bookings for a specific member
        /// </summary>
        /// <param name="membershipNumber">Member's unique identifier</param>
        /// <returns>List of member's bookings</returns>
        public async Task<List<Booking>> GetBookingsByMemberAsync(int membershipNumber)
        {
            return await _context.Bookings
                .Where(b => b.MembershipNumber == membershipNumber)
                .ToListAsync();
        }
    }
}