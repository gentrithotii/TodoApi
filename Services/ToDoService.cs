using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class ToDoService
{
    private readonly ToDoContext _context;

    public ToDoService(ToDoContext context)
    {
        _context = context;
    }

    public async Task<List<ToDoItem>> GetAllAsync()
    {
        return await _context.ToDoItems.ToListAsync();
    }

    public async Task<ToDoItem> GetByIdAsync(int id)
    {
        return await _context.ToDoItems.FindAsync(id);
    }

    public async Task<ToDoItem> AddAsync(ToDoItem item)
    {
        _context.ToDoItems.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<ToDoItem> UpdateAsync(ToDoItem item)
    {
        _context.Entry(item).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.ToDoItems.FindAsync(id);
        if (item == null)
        {
            return false;
        }

        _context.ToDoItems.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
