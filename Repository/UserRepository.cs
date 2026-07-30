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

        public async Task<UserModel> GetById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
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
            UserModel userId = await GetById(id);

            if (userId == null)
            {
                throw new Exception($"User with Id = {id} not found");
            }
            else
            {
                userId.Name = user.Name;
                userId.Email = user.Email;
                userId.UpdatedDate = DateTime.Now;
                _dbContext.Users.Update(userId);
                await _dbContext.SaveChangesAsync();
                return userId;
            }
        }

        public async Task<bool> Delete(int id)
        {
            UserModel userId = await GetById(id);

            if (userId == null)
            {
                throw new Exception($"User with Id = {id} not found");
            }
            else
            {
                _dbContext.Users.Remove(userId);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
