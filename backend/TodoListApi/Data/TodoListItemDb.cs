using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.Data
{
    public class TodoListItemDb : DbContext
    {
        public TodoListItemDb(DbContextOptions<TodoListItemDb> options)
        : base(options) { }

        public DbSet<TodoListItem> TodoListItems => Set<TodoListItem>();
    }
}
