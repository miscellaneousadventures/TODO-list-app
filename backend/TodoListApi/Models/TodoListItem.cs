namespace TodoListApi.Models
{
    public class TodoListItem
    {
        public Guid id { get; set; } = Guid.NewGuid();
        public string? name { get; set; } = string.Empty;
        public bool isCompleted { get; set; } = false;
        public string? username { get; set; }
    }
}
