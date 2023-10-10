using BooksWebAPI.DBUtility;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksWebAPI.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly BooksAPIDbContext _context;
        public MemberRepository(BooksAPIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Member>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }
    }
}
