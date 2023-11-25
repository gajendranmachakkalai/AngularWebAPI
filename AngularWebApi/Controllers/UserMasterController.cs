using AngularWebApi.DB.GenericRepository;
using AngularWebApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AngularWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserMasterController : ControllerBase
    {
        private readonly IRepository<User> _userRepository;

        public UserMasterController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userRepository.GetAll();
            return Ok(users.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddUsersAsync(User user)
        {
            await _userRepository.InsertAsync(user);
            return Ok(true);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
            return Ok(true);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = _userRepository.Get().Where(x => x.UserId == id).FirstOrDefault();
            if (user != null)
               await _userRepository.DeleteAsync(user);
            return Ok(true);
        }
    }
}
