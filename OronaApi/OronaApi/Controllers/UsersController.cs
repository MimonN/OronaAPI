using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OronaApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public UsersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginRespone = await _unitOfWork.LocalUser.LoginAsync(model);
            if(loginRespone.User == null || string.IsNullOrEmpty(loginRespone.Token))
            {
                return BadRequest(new {message = "Username or password is incorrect"});
            }

            return Ok(loginRespone);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            bool ifUserNameUnique = await _unitOfWork.LocalUser.IsUniqueUserAsync(model.UserName);
            if(!ifUserNameUnique)
            {
                return BadRequest(new {message = "Username already exists"});
            }

            var user = await _unitOfWork.LocalUser.RegisterAsync(model);
            if(user == null)
            {
                return BadRequest(new { message = "Error while registering" });
            }

            return Ok();
        }
    }
}
