using Domain.Commons.Models;

namespace Domain.ToDoItems
{
    public class ToDoItem : Entity<int>
    {
        public string Name { get; protected set; }
        public string? Description { get; protected set; }
        public ToDoItemPriority Priority { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public bool IsDone { get; protected set; }
    }
}
