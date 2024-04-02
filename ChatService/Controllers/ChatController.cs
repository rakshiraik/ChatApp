using ChatBot.Common.ViewModels;
using ChatService.Repository;
using ChatService.Repository.Contracts;
using ChatService.Services.Error;
using ChatService.Services.RoomUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChatService.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        public IRoomUserService _roomUserService;
        public IErrorService _errorService;


        public  ChatController(IRoomUserService roomRepository, IErrorService errorService)
        {
            _roomUserService = roomRepository;
            _errorService = errorService;
   
        }

        // GET: api/<ChatController>
        [HttpGet]
        public IActionResult Get()
        {

            var rooms = _roomUserService.GetAll();
            return Ok(rooms);
        }


        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            var rooms = _roomUserService.GetById(id);
            return Ok(rooms);
        }


        [HttpPost("RegisterRoom")]
        public async Task<IActionResult> RegisterUser(RoomViewModel model)
        {
            UserResultViewModel userResultViewModel = new UserResultViewModel();
            string result = await _roomUserService.CreateRoomAsync(model);
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
