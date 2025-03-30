using Application.Commons;
using Application.ToDoItems.DTOs;
using Application.ToDoItems.Specs;
using Domain.ToDoItems;
using Infrastructure.Exceptions;
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
            if (string.IsNullOrWhiteSpace(dto.name))
                throw new BadRequestException(ErrorMessages.REQUIRED_TO_DO_ITEM_NAME);

            if (!Enum.IsDefined(typeof(ToDoItemPriority), dto.priority))
                throw new BadRequestException(ErrorMessages.INVALID_TO_DO_ITEM_PRIORITY);

            var item = ToDoItem.Create(
                dto.name,
                dto.description,
                dto.priority);

            await _toDoItemRepo.CreateToDoItemAsync(item);
            await _uow.CommitAsync();
        }

        public async Task UpdateToDoItemAsync(int id)
        {
            var item = await _toDoItemRepo.GetToDoItemAsync(id);
            if (item is null)
                throw new NotFoundException(ErrorMessages.NOT_FOUND_TO_DO_ITEM);

            if (item.IsDone)
                throw new BadRequestException(ErrorMessages.ALREADY_DONE_TO_DO_ITEM);

            item.MarkAsDone();

            await _uow.CommitAsync();
        }
    }
}
