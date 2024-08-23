using ToDoApi.Models;

namespace ToDoApi.Services.InterfaceServices
{
    public interface IToDoService
    {
        Task<List<ToDoItem>> GetAllToDosAsync();
        Task<ToDoItem> GetToDoByIdAsync(int id);
        Task<ToDoItem> AddToDoAsync(ToDoItem item);
        Task<ToDoItem> UpdateToDoAsync(ToDoItem item);
        Task<bool> DeleteToDoByIdAsync(int id);
    }
}