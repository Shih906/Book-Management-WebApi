using BooksWebAPI.DBUtility;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksWebAPI.Repository
{
    public class CodeRepository : ICodeRepository
    {
        private readonly BooksAPIDbContext _context;
        public CodeRepository(BooksAPIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Code>> GetCodes()
        {
            return await _context.Codes.ToListAsync();
        }
    }
}
