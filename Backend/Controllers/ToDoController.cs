using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Services.InterfaceServices;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        private int? GetUserIdFromClaims()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return null;
            }
            return userId;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
        {
            return Ok(await _toDoService.GetAllToDosAsync());
        }

        [Authorize]
        [HttpGet("Todos:{id}")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItemsForUser(int id)
        {
            var userId = GetUserIdFromClaims();

            if (userId == null || userId != id) return Unauthorized("No Access!");

            var item = await _toDoService.GetToDoItemsForUserAsync(userId.Value);

            return Ok(item);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetById(int id)
        {
            var item = await _toDoService.GetToDoByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [Authorize]
        [HttpPost("AddToDoItem")]
        public async Task<IActionResult> AddToDoItem([FromBody] ToDoItem reqToDoItem)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userId = GetUserIdFromClaims();

            if (userId == null) return Unauthorized("No Access!");

            var newToDoItem = await _toDoService.AddToDoItemForUserAsync(userId.Value, reqToDoItem);

            return Ok(newToDoItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToDoItem item)
        {
            if (id != item.Id) return BadRequest("ID mismatch");

            var updatedItem = await _toDoService.UpdateToDoAsync(id, item.Id, item);
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _toDoService.DeleteToDoByIdAsync(id);

            if (!success) return NotFound();

            return NoContent();
        }
    }
}
