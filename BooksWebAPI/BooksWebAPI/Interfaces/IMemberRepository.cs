using BooksWebAPI.Models;

namespace BooksWebAPI.Interfaces
{
    public interface IMemberRepository
    {
        Task<ICollection<Member>> GetMembers();
    }
}
