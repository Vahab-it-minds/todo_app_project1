using backend.repositories;
using System.Security.Claims;
using backend.models;
using backend.classes;

namespace backend.endpoints
{
    public static class TodoEndpoints
    {
        public static void MapTodoEndpoints(this WebApplication app)
        {
            app.MapGet("/todos", async (
                HttpContext context,
                TodoRepository todoRepository) =>
            {
                var userIdValue = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userId = int.Parse(userIdValue!);

                var todos = await todoRepository.GetTodosByUserId(userId);

                return Results.Ok(todos);
            })
            .RequireAuthorization();

            app.MapGet("/todos/{id}", async (
                int id,
                HttpContext context,
                TodoRepository todoRepository) =>
            {

                var userIdValue = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var userId = int.Parse(userIdValue!);

                var todo = await todoRepository.GetTodoById(id);

                if (todo == null)
                {
                    return Results.NotFound();
                }

                if (todo.UserId != userId)
                {
                    return Results.NotFound();
                }

                return Results.Ok(todo);
            })
            .RequireAuthorization();

            app.MapPost("/todos", async (
                CreateTodoRequest request,
                HttpContext context,
                TodoRepository todoRepository) =>
            {
                var userIdValue = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userId = int.Parse(userIdValue!);

                var now = DateTime.Now;

                var todo = new Todo(
                    0,
                    request.Title,
                    request.Description,
                    now,
                    now,
                    request.DueDate,
                    userId,
                    request.Priority,
                    request.TimeEstimate,
                    request.Category,
                    TodoStatus.Todo
                );

                await todoRepository.CreateTodo(todo);

                return Results.Ok(todo);
            })
            .RequireAuthorization();

            app.MapPut("/todos/{id}", async (
                int id,
                UpdateTodoRequest request,
                HttpContext context,
                TodoRepository todoRepository) =>
            {
                
                var userIdValue = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;
                
                var userId = int.Parse(userIdValue!);

                var todo = await todoRepository.GetTodoById(id);

                if (todo == null)
                {
                    return Results.NotFound();
                }

                if (todo.UserId != userId)
                {
                    return Results.NotFound();
                }

                todo.SetTitle(request.Title);
                todo.SetDescription(request.Description);
                todo.SetDueDate(request.DueDate);
                todo.SetPriority(request.Priority);
                todo.SetTimeEstimate(request.TimeEstimate);
                todo.SetCategory(request.Category);
                todo.SetStatus(request.Status);

                await todoRepository.UpdateTodo(todo);

                return Results.Ok(todo);
            })
            .RequireAuthorization();

            app.MapDelete("/todos/{id}", async (
                int id,
                HttpContext context,
                TodoRepository todoRepository) =>
            {
                var userIdValue = context.User
                    .FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var userId = int.Parse(userIdValue!);

                var todo = await todoRepository.GetTodoById(id);

                if (todo == null)
                {
                    return Results.NotFound();
                }

                if (todo.UserId != userId)
                {
                    return Results.NotFound();
                }

                await todoRepository.DeleteTodo(id);

                return Results.NoContent();
            })
            .RequireAuthorization();

        }
    }
}