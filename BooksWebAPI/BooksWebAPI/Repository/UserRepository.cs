using AutoMapper.Configuration.Conventions;
using BooksWebAPI.DBUtility;
using BooksWebAPI.Interfaces;
using BooksWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BooksWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BooksAPIDbContext _context;
        public UserRepository(BooksAPIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<User>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByName(string username)
        {
            return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
        }
        public async Task<User> GetUser(string userName, string passwordHash)
        {
            return await _context.Users.Where(u => u.Username == userName && u.PasswordHash == passwordHash).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserExist(string username)
        {
            var result = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            if (result == null)
            {
                return false;
            }
            return true;
        }

        public async Task<int> CreateUser(User user)
        {
            _context.Users.Add(user);

            return await _context.SaveChangesAsync();
        }

        public bool IsUserExists(int Id)
        {

            return _context.Users.Any(u => u.Id == Id);
        }

        public async Task<bool> UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return true;
        }

        public async Task <string> GetUserPassHash(int Id)
        {
            return _context.Users.Where(u => u.Id == Id).Select(u => u.PasswordHash).First().ToString();
        }
    }
}
