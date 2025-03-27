using Application.Commons;
using Application.ToDoItems.DTOs;
using Application.ToDoItems.Specs;
using Domain.ToDoItems;
using Mapster;

namespace Application.ToDoItems
{
    public class ToDoItemService
    {
        private readonly IToDoItemRepository _toDoItemRepo;
        private readonly IUnitOfWork _uow;

        public ToDoItemService(
            IToDoItemRepository toDoItemRepo,
            IUnitOfWork uow
            )
        {
            _toDoItemRepo = toDoItemRepo;
            _uow = uow;
        }

        public async Task<List<ToDoItemListDTO>> ListToDoItemsAsync(bool isDone)
        {
            var items = await _toDoItemRepo.ListToDoItemsAsync(new ToDoItemIsDoneSpec(isDone));
            return items.Adapt<List<ToDoItemListDTO>>();
        }

        public async Task CreateToDoItemAsync(ToDoItemCreateDTO dto)
        {
            var item = ToDoItem.Create(
                dto.name,
                dto.description,
                dto.priority);

            await _toDoItemRepo.CreateToDoItemAsync(item);
            await _uow.CommitAsync();
        }
    }
}
