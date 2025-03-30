using Application.ToDoItems.DTOs;
using Domain.ToDoItems;
using Mapster;

namespace Application.ToDoItems.ToDoItemTypeMappings
{
    public class ToDoItemTypeMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            // Entity -> DTO
            config.NewConfig<ToDoItem, ToDoItemListDTO>()
                .MapWith(src => new ToDoItemListDTO
                {
                    id = src.Id,
                    name = src.Name,
                    description = src.Description,
                    priority = src.Priority,
                    createdAt = src.CreatedAt,
                    isDone = src.IsDone
                });
        }
    }
}
