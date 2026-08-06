using Microsoft.AspNetCore.Mvc;
using TaskSystem.Models;
using TaskSystem.Repository.Interfaces;

namespace TaskSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetAll()
        {
            List<UserModel> users = await _userRepository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetById(int id)
        {
            UserModel? user = await _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound($"User with Id = {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<UserModel>> Create([FromBody] UserModel userModel)
        {
            UserModel user = await _userRepository.Create(userModel);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> Update([FromBody] UserModel userModel, int id)
        {
            try
            {
                UserModel user = await _userRepository.Update(userModel, id);

                return Ok(user);
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
                await _userRepository.Delete(id);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}