using BooksWebAPI.DBUtility;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksWebAPI.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksAPIDbContext _context;
        public BookRepository(BooksAPIDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Book>> GetBooks() 
        {
            return await _context.Books.ToListAsync();
            //return _context.Books.Include(b => b.Class).Include(b => b.Code).Include(b => b.Member).ToList();
            //return (ICollection<Book>)_context.Books.Include(b => b.Class).Include(b => b.Code).Include(b => b.Member)
                //.Select(b => new { b.Name , b.Author}).ToList();
        }
        public async Task<ICollection<Book>> GetBookRltData()
        {
            return await _context.Books.Include(b => b.Class).Include(b => b.Code).Include(b => b.Member).ToListAsync();
        }

        public async Task<Book> GetBook(int id)
        {
            return await _context.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Book> GetBook(string name)
        {
            return await _context.Books.Where(b => b.Name == name).FirstOrDefaultAsync();
        }
        public bool IsBookExists(int Id)
        {
           
            return _context.Books.Any(b => b.Id == Id);
        }

        public bool CreateBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChangesAsync();

            return true;
        }

        public bool UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public bool Save() 
        {
            var result = _context.SaveChanges();
            return result > 0 ? true : false;
        }
    }
}
