using TodoListApi.Models;

namespace TodoListApi.DTOs
{
    public class TodoListItemDTO
    {
        public Guid id { get; set; }
        public string? name { get; set; }
        public bool isCompleted { get; set; }

        public TodoListItemDTO() { }
        public TodoListItemDTO(TodoListItem todoListItem) =>
        (id, name, isCompleted) = (todoListItem.id, todoListItem.name, todoListItem.isCompleted);
    }
}
