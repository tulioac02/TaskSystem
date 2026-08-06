using TaskSystem.Models;

namespace TaskSystem.Repository.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskModel>> GetAll();
        Task<TaskModel> GetById(int id);
        Task<TaskModel> Create(TaskModel Task);
        Task<TaskModel> Update(TaskModel Task, int id);
        Task<bool> Delete(int id);
    }
}
