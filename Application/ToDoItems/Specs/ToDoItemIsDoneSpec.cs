using Domain.Commons;
using Domain.ToDoItems;
using System.Linq.Expressions;

namespace Application.ToDoItems.Specs
{
    public class ToDoItemIsDoneSpec : Specification<ToDoItem>
    {
        private readonly bool _isDone;
        public ToDoItemIsDoneSpec(bool isDone)
        {
            _isDone = isDone;
        }

        public override Expression<Func<ToDoItem, bool>> ToExpressAll()
        {
            return item => item.IsDone == _isDone;
        }
    }
}
