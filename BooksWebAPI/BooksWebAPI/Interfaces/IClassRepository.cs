using BooksWebAPI.Models;

namespace BooksWebAPI.Interfaces
{
    public interface IClassRepository
    {
        Task<ICollection<Class>> GetClasses();
    }
}
