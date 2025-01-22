using Microsoft.AspNetCore.Mvc;
using NotesApi.Core.Entities;
using NotesApi.Services.Interfaces;
using NotesApi.Services.Jwt;

namespace NotesApi.Controllers
{
    [ApiController]
    public class NotesController(IUserService _userService, INoteService _noteService, IConfiguration _config) : ControllerBase
    {
        [HttpPost]
        [Route("CreateAccount/{email}-{password}-{nickname}")]
        public async Task<IActionResult> CreateAccount(string email, string password, string nickname)
        {
            await _userService.CreateAccount(email, password, nickname);
            return Ok("Аккаунт создан");
        }

        [HttpPost]
        [Route("CreateNote/{userId}-{name}-{description}")]
        public async Task<IActionResult> CreateNote(Guid userId, string name, string description)
        {
            await _noteService.CreateNoteAsync(userId, name, description);
            return Ok("Заметка создана");
        }

        [HttpGet]
        [Route("LoginUser/{email}-{password}")]
        public async Task<User?>LoginUser(string email, string password)
        {
            var user = await _userService.ValidateUser(email, password);
            if (user != null)
            {
                var token = JwtBuilder.GenerateJwtToken(user.Nickname, user.Id.ToString(), _config);
                JwtBuilder.AppendJwtCookie(token, Response);
                return user;
            }
            return null;
        }

        [HttpPut]
        [Route("UpdateNote/{id}-{description}")]
        public async Task<IActionResult>UpdateNote(Guid id, string description)
        {
            await _noteService.UpdatedNoteAsync(id, description);
            return Ok("Заметка обновлена");
        }

        [HttpGet]
        [Route("GetAllNotes/{userId}")]
        public async Task<List<Note>> GetAllNotes(Guid userId)
        {
            return await _noteService.GetAllNotesAsync(userId);
        }
    }
}
