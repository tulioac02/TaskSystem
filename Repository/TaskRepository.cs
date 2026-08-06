using TaskSystem.Data;
using TaskSystem.Models;
using TaskSystem.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskSystem.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskSystemDBContext _dbContext;

        public TaskRepository(TaskSystemDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TaskModel>> GetAll()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<TaskModel> GetById(int id)
        {
            return await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TaskModel> Create(TaskModel task)
        {
            task.CreatedDate = DateTime.Now;
            await _dbContext.Tasks.AddAsync(task);
            await _dbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskModel> Update(TaskModel task, int id)
        {
            TaskModel taskById = await GetById(id);

            if (taskById == null)
            {
                throw new Exception($"Task with Id = {id} not found");
            }
            else
            {
                taskById.Name = task.Name;
                taskById.Description = task.Description;
                taskById.Status = task.Status;
                taskById.UserId = task.UserId;
                taskById.UpdatedDate = DateTime.Now;

                _dbContext.Tasks.Update(taskById);
                await _dbContext.SaveChangesAsync();
                return taskById;
            }
        }

        public async Task<bool> Delete(int id)
        {
            TaskModel taskById = await GetById(id);

            if (taskById == null)
            {
                throw new Exception($"Task with Id = {id} not found");
            }
            else
            {
                _dbContext.Tasks.Remove(taskById);
                await _dbContext.SaveChangesAsync();
                return true;
            }
        }
    }
}
