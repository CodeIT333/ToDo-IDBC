using Domain.Commons;
using Domain.ToDoItems;

namespace Application.ToDoItems
{
    public interface IToDoItemRepository
    {
        Task<List<ToDoItem>> ListToDoItemsAsync(Specification<ToDoItem> spec);
        Task<ToDoItem> GetToDoItemAsync(int id);
        Task CreateToDoItemAsync(ToDoItem toDoItem);
    }
}
