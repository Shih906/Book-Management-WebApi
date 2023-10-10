using BooksWebAPI.DBUtility;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksWebAPI.Repository
{
    public class ClassRepository : IClassRepository
    {
        private readonly BooksAPIDbContext _context;
        public ClassRepository(BooksAPIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Class>> GetClasses()
        {
            return await _context.Classes.ToListAsync();
        }
    }
}
