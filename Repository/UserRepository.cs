using TaskSystem.Data;
using TaskSystem.Models;
using TaskSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskSystem.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TaskSystemDBContext _dbContext;

        public UserRepository(TaskSystemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel?> GetById(int id)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Create(UserModel user)
        {
            user.CreatedDate = DateTime.Now;

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel? userById = await GetById(id);

            if (userById == null)
            {
                throw new KeyNotFoundException($"User with Id = {id} not found.");
            }

            userById.Name = user.Name;
            userById.Email = user.Email;
            userById.UpdatedDate = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return userById;
        }

        public async Task<bool> Delete(int id)
        {
            UserModel? userById = await GetById(id);

            if (userById == null)
            {
                throw new KeyNotFoundException($"User with Id = {id} not found.");
            }

            _dbContext.Users.Remove(userById);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}