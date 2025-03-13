using Microsoft.EntityFrameworkCore;
using PortmarnockGolfClub.Data;
using PortmarnockGolfClub.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortmarnockGolfClub.Services
{
    public class MemberService
    {
        private readonly GolfClubDbContext _context;

        public MemberService(GolfClubDbContext context)
        {
            _context = context;
        }

        // Get all members
        public async Task<List<Member>> GetAllMembersAsync()
        {
            return await _context.Members.ToListAsync();
        }

        // Get member by ID
        public async Task<Member> GetMemberByIdAsync(int membershipNumber)
        {
            return await _context.Members.FindAsync(membershipNumber);
        }

        // Add new member
        public async Task<Member> AddMemberAsync(Member member)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync();
            return member;
        }

        // Update member
        public async Task<bool> UpdateMemberAsync(Member member)
        {
            _context.Entry(member).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete member
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