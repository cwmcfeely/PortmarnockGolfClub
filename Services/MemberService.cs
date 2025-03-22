using Microsoft.EntityFrameworkCore;
using PortmarnockGolfClub.Data;
using PortmarnockGolfClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortmarnockGolfClub.Services
{
    /// <summary>
    /// Service class for managing Member entities in the database
    /// </summary>
    public class MemberService
    {
        private readonly GolfClubDbContext _context;

        /// <summary>
        /// Constructor that injects the database context
        /// </summary>
        /// <param name="context">The database context</param>
        public MemberService(GolfClubDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all members from the database
        /// </summary>
        /// <returns>A list of all members</returns>
        public async Task<List<Member>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific member by their membership number
        /// </summary>
        /// <param name="membershipNumber">The unique identifier for the member</param>
        /// <returns>The member if found, otherwise null</returns>
        public async Task<Member?> GetMemberByIdAsync(int membershipNumber)
        {
            return await _context.Members.FindAsync(membershipNumber);
        }

        /// <summary>
        /// Adds a new member to the database
        /// </summary>
        /// <param name="member">The member entity to add</param>
        /// <returns>The added member with updated information (e.g., assigned ID)</returns>
        public async Task<Member> AddMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        /// <summary>
        /// Updates an existing member in the database
        /// </summary>
        /// <param name="member">The member entity with updated information</param>
        /// <returns>True if the update was successful, false otherwise</returns>
        public async Task<bool> UpdateMemberAsync(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Deletes a member from the database
        /// </summary>
        /// <param name="membershipNumber">The membership number of the member to delete</param>
        /// <returns>True if the member was successfully deleted, false if the member was not found</returns>
        public async Task<bool> DeleteMemberAsync(int membershipNumber)
        {
            var member = await _context.Members.FindAsync(membershipNumber);
            if (member == null)
                return false;

            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
