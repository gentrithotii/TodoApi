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
            if (item == null)
            {
                return null;
            }
            return item;
        }
        public async Task<ToDoItem> AddToDoItemForUserAsync(int reqUserId, ToDoItem reqToDoItem)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == reqUserId);
            if (!userExists)
            {
                return null;
            }

            ToDoItem item = new()
            {
                Title = reqToDoItem.Title,
                Description = reqToDoItem.Description,
                IsCompleted = reqToDoItem.IsCompleted,
                UserId = reqUserId,
            };

            await _context.SaveChangesAsync();
            return item;
        }
        public async Task<List<ToDoItem>> GetToDoItemsForUserAsync(int userId)
        {
            var items = await _context.ToDoItems.Where((item) => item.UserId == userId).ToListAsync();

            return items;
        }

        public async Task<ToDoItem> AddToDoAsync(ToDoItem item)
        {
            _context.ToDoItems.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<ToDoItem> UpdateToDoAsync(ToDoItem item)
        {
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteToDoByIdAsync(int id)
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

}
