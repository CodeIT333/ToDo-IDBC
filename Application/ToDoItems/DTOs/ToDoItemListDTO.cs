using Domain.ToDoItems;

namespace Application.ToDoItems.DTOs
{
    public class ToDoItemListDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public ToDoItemPriority priority { get; set; }
        public DateTime createdAt { get; set; }
        public bool isDone { get; set; }
    }
}
