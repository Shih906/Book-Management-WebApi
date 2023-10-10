using BooksWebAPI.Models;

namespace BooksWebAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsers();

        Task<User> GetUserByName(string username);
        Task<User> GetUser(string username, string passwordHash);

        Task<bool> IsUserExist(string username);

        Task<int> CreateUser(User user);

        bool IsUserExists(int Id);

        Task<bool> UpdateUser(User user);

        Task<string> GetUserPassHash(int Id);


    }
}
