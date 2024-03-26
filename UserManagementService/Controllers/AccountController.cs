using ChatBot.Common.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UserManagementService.Services.Error;
using UserManagementService.Services.User;

namespace UserManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IErrorService _errorService;
        public AccountController(IUserService userService, IErrorService errorService)
        {
            _userService = userService;
            _errorService = errorService;
        }


        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterViewModel model)
        {
            UserResultViewModel userResultViewModel = new UserResultViewModel();
            string result = await _userService.CreateUserAsync(model.Email, model.Password);
            if (!string.IsNullOrEmpty(result))
            {
                userResultViewModel = JsonConvert.DeserializeObject<UserResultViewModel>(result)!;
                return Ok(userResultViewModel);
            }
            else
            {
                return BadRequest(this._errorService.ErrorContainer);
            }           
        }

  

    }
}
