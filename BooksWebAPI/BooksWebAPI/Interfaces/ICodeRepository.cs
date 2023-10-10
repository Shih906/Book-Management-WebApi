using BooksWebAPI.Models;

namespace BooksWebAPI.Interfaces
{
    public interface ICodeRepository
    {
        Task<ICollection<Code>> GetCodes();
    }
}
