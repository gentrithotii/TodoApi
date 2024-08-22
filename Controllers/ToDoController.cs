using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly ToDoService _toDoService;

    public ToDoController(ToDoService toDoService)
    {
        _toDoService = toDoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItem>>> GetAll()
    {
        return Ok(await _toDoService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ToDoItem>> GetById(int id)
    {
        var item = await _toDoService.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItem>> Create(ToDoItem item)
    {
        var createdItem = await _toDoService.AddAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = createdItem.Id }, createdItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ToDoItem item)
    {
        if (id != item.Id)
        {
            return BadRequest();
        }

        var updatedItem = await _toDoService.UpdateAsync(item);
        return Ok(updatedItem);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _toDoService.DeleteAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}
