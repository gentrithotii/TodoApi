using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;
using ToDoApi.Models.Dtos;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
        {
            return Ok(await _toDoService.GetAllToDosAsync());
        }

        [HttpGet("todos{id}")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItemsForUser(int id)
        {
            var item = await _toDoService.GetToDoItemsForUserAsync(id);

            return Ok(item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ToDoItem>> GetById(int id)
        {
            var item = await _toDoService.GetToDoByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost("AddToDoItem")]
        public async Task<IActionResult> AddToDoItem([FromBody] ToDoItem reqToDoItem)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                return Unauthorized("No Access!");
            }


            var newToDoItem = await _toDoService.AddToDoItemForUserAsync(userId, reqToDoItem);
            return Ok(newToDoItem);
        }

        [HttpPost]
        public async Task<ActionResult<ToDoItem>> Create(ToDoItem item)
        {
            var createdItem = await _toDoService.AddToDoAsync(item);
            return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ToDoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            var updatedItem = await _toDoService.UpdateToDoAsync(item);
            return Ok(updatedItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _toDoService.DeleteToDoByIdAsync(id);
            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
