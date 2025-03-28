namespace APP.Models
{
    public class ToDoItemCreate
    {
        public string name { get; set; }
        public string? description { get; set; }
        public byte priority { get; set; }
    }
}
