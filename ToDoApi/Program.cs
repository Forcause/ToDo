using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Model;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ToDoApplicationContext>(opt =>
opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnectionString")));

var app = builder.Build();


app.MapGet("api/todo", async (ToDoApplicationContext context) =>
{
    var items = await context.ToDos.ToListAsync();
    await context.DisposeAsync();
    return Results.Ok(items);
});

app.MapPost("api/todo", async (ToDoApplicationContext context, ToDoModel toDo) =>
{
    if (toDo.ToDoName == null) return Results.Problem("Name cannot be null");
    await context.AddAsync(toDo);

    await context.SaveChangesAsync();

    return Results.Created($"api/todo/{toDo.Id}", toDo);
});

app.MapPut("api/todo/{id}", async (ToDoApplicationContext context, int id, ToDoModel toDo) =>
{
    var toDoModelFromDb = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
    if (toDoModelFromDb == null) return Results.NotFound();

    toDoModelFromDb.ToDoDescription = toDo.ToDoDescription;
    toDoModelFromDb.ToDoName = toDo.ToDoName;

    await context.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("api/todo/del/{id}", async (ToDoApplicationContext context, int id) =>
{
    var toDoModelFromDb = await context.ToDos.FirstOrDefaultAsync(t => t.Id == id);
    if (toDoModelFromDb == null) return Results.NotFound();

    context.ToDos.Remove(toDoModelFromDb);

    await context.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("api/todo/trunc", async (ToDoApplicationContext context) =>
{
    await context.Database.ExecuteSqlRawAsync("DELETE FROM ToDos");

    return Results.Ok();
});

app.Run();
