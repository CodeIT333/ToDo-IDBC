using Domain.ToDoItems;

namespace UnitTest.ToDoItems.Testables
{
    internal class TestableToDoItem : ToDoItem
    {
        public TestableToDoItem(
            string name,
            string description,
            ToDoItemPriority priority,
            DateTime createdAt,
            bool isDone
            )
        {
            Name = name;
            Description = description;
            Priority = priority;
            CreatedAt = createdAt;
            IsDone = isDone;
        }
    }
}
