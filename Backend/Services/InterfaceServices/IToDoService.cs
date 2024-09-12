using ToDoApi.Models;

namespace ToDoApi.Services.InterfaceServices
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllToDosAsync();
        Task<ToDoItem?> GetToDoByIdAsync(int id);
        Task<List<ToDoItem>> GetToDoItemsForUserAsync(int user);
        Task<ToDoItem> AddToDoItemForUserAsync(int user, ToDoItem toDoItem);
        Task<ToDoItem?> UpdateToDoAsync(int userId, int toDoId, ToDoItem updatedItem);
        Task<bool> DeleteToDoByIdAsync(int id);
    }
}