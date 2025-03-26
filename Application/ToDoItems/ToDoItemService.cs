using Application.Commons;
using Application.ToDoItems.DTOs;
using Application.ToDoItems.Specs;
using Mapster;

namespace Application.ToDoItems
{
    // TODO DB: item id identity torlese (lehet random int ertekeket generalni, mint guid-nal? -ha nem, akkor maradjon, es toroljuk a valuenotgenerated kitetelt)
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
    }
}
