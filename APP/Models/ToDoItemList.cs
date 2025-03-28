namespace APP.Models
{
    public class ToDoItemList
    {
        public int id { get; set; }
        public string name { get; set; }
        public string? description { get; set; }
        public ToDoItemPriority priority { get; set; }
        public DateTime createdAt { get; set; }
        public bool isDone { get; set; }
    }
}
