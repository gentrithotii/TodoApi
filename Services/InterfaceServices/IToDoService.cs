using ToDoApi.Models;

namespace ToDoApi.Services.InterfaceServices
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllToDosAsync();
        Task<ToDoItem?> GetToDoByIdAsync(int id);
        Task<List<ToDoItem>> GetToDoItemsForUserAsync(int user);
        Task<ToDoItem?> AddToDoItemForUserAsync(int userId, ToDoItem toDoItem);
        Task<ToDoItem> AddToDoAsync(ToDoItem item);
        Task<ToDoItem> UpdateToDoAsync(ToDoItem item);
        Task<bool> DeleteToDoByIdAsync(int id);
    }
}