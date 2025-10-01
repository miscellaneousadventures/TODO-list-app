using TodoListApi.Models;
using TodoListApi.Data; 
using TodoListApi.DTOs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoListItemDb>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddOpenApi("ToDoList");

// connect to our Angular application
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:4200") 
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => "ToDoList API is running!");

var todoListItems = app.MapGroup("/todolistitems");

todoListItems.MapGet("/", GetAllTodoListItems);
todoListItems.MapGet("/complete", GetCompleteTodoListItems);
todoListItems.MapGet("/{id}", GetTodoListItem);
todoListItems.MapPost("/", CreateTodoListItem);
todoListItems.MapPut("/{id}", UpdateTodoListItem);
todoListItems.MapDelete("/{id}", DeleteTodoListItem);

app.Run();

static async Task<IResult> GetAllTodoListItems(TodoListItemDb db)
{
    return TypedResults.Ok(await db.TodoListItems.Select(x => new TodoListItemDTO(x)).ToArrayAsync());
}

static async Task<IResult> GetCompleteTodoListItems(TodoListItemDb db)
{
    return TypedResults.Ok(await db.TodoListItems.Where(t => t.isCompleted).Select(x => new TodoListItemDTO(x)).ToListAsync());
}

static async Task<IResult> GetTodoListItem(Guid id, TodoListItemDb db)
{
    return await db.TodoListItems.FindAsync(id)
        is TodoListItem todolistitem
            ? TypedResults.Ok(new TodoListItemDTO(todolistitem))
            : TypedResults.NotFound();
}

static async Task<IResult> CreateTodoListItem(TodoListItemDTO todoListItemDTO, TodoListItemDb db)
{
    var todoListItem = new TodoListItem
    {
        isCompleted = todoListItemDTO.isCompleted,
        name = todoListItemDTO.name
    };

    db.TodoListItems.Add(todoListItem);
    await db.SaveChangesAsync();

    todoListItemDTO = new TodoListItemDTO(todoListItem);

    return TypedResults.Created($"/todolistitems/{todoListItem.id}", todoListItemDTO);
}

static async Task<IResult> UpdateTodoListItem(Guid id, TodoListItemDTO todoListItemDTO, TodoListItemDb db)
{
    var todoListItem = await db.TodoListItems.FindAsync(id);

    if (todoListItem is null) return TypedResults.NotFound();

    todoListItem.name = todoListItemDTO.name;
    todoListItem.isCompleted = todoListItemDTO.isCompleted;

    await db.SaveChangesAsync();

    return TypedResults.NoContent();
}

static async Task<IResult> DeleteTodoListItem(Guid id, TodoListItemDb db)
{
    if (await db.TodoListItems.FindAsync(id) is TodoListItem todoListItem)
    {
        db.TodoListItems.Remove(todoListItem);
        await db.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound();
}
