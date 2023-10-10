using BooksWebAPI.Models;

namespace BooksWebAPI.Interfaces
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetBooks();

        Task<ICollection<Book>> GetBookRltData();

        Task<Book> GetBook(int id);

        Task<Book> GetBook(string name);
        bool IsBookExists(int Id);
        bool CreateBook(Book book);

        bool UpdateBook(Book book);

        bool DeleteBook(Book book);

        //ICollection<Book> GetAllBooks();

        //ICollection<Book> GetAllBooks(int? pageIndex = null);


    }
}
