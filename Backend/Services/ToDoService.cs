using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using ToDoApi.Services.InterfaceServices;


namespace ToDoApi.Services
{
    public class ToDoService : IToDoService
    {
        private readonly ToDoContext _context;

        public ToDoService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> GetAllToDosAsync()
        {
            return await _context.ToDoItems.ToListAsync();
        }

        public async Task<ToDoItem?> GetToDoByIdAsync(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);

            if (item == null) return null;

            return item;
        }

        public async Task<ToDoItem> AddToDoItemForUserAsync(int reqUserId, ToDoItem reqToDoItem)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == reqUserId);

            if (!userExists) throw new InvalidOperationException("User does not exist");

            ToDoItem item = new()
            {
                Title = reqToDoItem.Title,
                Description = reqToDoItem.Description,
                IsCompleted = reqToDoItem.IsCompleted,
                UserId = reqUserId
            };
            _context.ToDoItems.Add(item);

            await _context.SaveChangesAsync();

            return item;
        }

        public async Task<List<ToDoItem>> GetToDoItemsForUserAsync(int userId)
        {
            var items = await _context.ToDoItems.Where((item) => item.UserId == userId).ToListAsync();

            return items;
        }

        public async Task<ToDoItem?> UpdateToDoAsync(int userId, int toDoId, ToDoItem updatedItem)
        {

            var existingItem = await _context.ToDoItems.FirstOrDefaultAsync(item => item.Id == toDoId && item.UserId == userId);

            if (existingItem == null) return null;

            existingItem.Title = updatedItem.Title ?? existingItem.Title;
            existingItem.Description = updatedItem.Description ?? existingItem.Description;
            existingItem.IsCompleted = updatedItem.IsCompleted;

            _context.Entry(existingItem).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return existingItem;
        }

        public async Task<bool> DeleteToDoByIdAsync(int id)
        {
            var item = await _context.ToDoItems.FindAsync(id);
            if (item == null) return false;

            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}
