namespace Infrastructure.Exceptions
{
    public abstract class ToDoAPIException : Exception
    {
        public int StatusCode { get; }
        public ToDoAPIException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
