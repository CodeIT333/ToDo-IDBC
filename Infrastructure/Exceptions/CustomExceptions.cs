namespace Infrastructure.Exceptions
{
    public class NotFoundException : ToDoAPIException
    {
        public NotFoundException(string message) : base(message, 404) { }
    }

    public class BadRequestException : ToDoAPIException
    {
        public BadRequestException(string message) : base(message, 400) { }
    }
}
