using Application.ToDoItems;
using Domain.Commons;
using Domain.ToDoItems;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories.ToDoItems
{
    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoContext _dbContext;
        public ToDoItemRepository(ToDoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ToDoItem>> ListToDoItemsAsync(Specification<ToDoItem> spec) => 
            await _dbContext.ToDoItem.Where(spec.ToExpressAll()).OrderByDescending(i => i.Priority).ThenByDescending(i => i.CreatedAt).ToListAsync();
   
        public async Task CreateToDoItemAsync(ToDoItem toDoItem) => await _dbContext.ToDoItem.AddAsync(toDoItem);
    }
}
