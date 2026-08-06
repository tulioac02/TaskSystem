using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TasksController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskModel>>> GetAll()
        {
            List<TaskModel> tasks = await _taskRepository.GetAll();

            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetById(int id)
        {
            TaskModel? task = await _taskRepository.GetById(id);

            if (task == null)
            {
                return NotFound($"Task with Id = {id} not found.");
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> Create([FromBody] TaskModel taskModel)
        {
            TaskModel task = await _taskRepository.Create(taskModel);

            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TaskModel>> Update([FromBody] TaskModel taskModel, int id)
        {
            try
            {
                TaskModel task = await _taskRepository.Update(taskModel, id);

                return Ok(task);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _taskRepository.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}