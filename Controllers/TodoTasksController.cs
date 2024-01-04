using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using todo_list_backend.Data;
using todo_list_backend.Models.Entities;

namespace todo_list_backend.Controllers
{
    [ApiController]
    [Route("api/")]
    [Authorize]
    public class TodoTasksController : Controller
    {
        private readonly TodoTasksDbContext todoTasksDbContext;

        public TodoTasksController(TodoTasksDbContext todoTasksDbContext)
        {
            this.todoTasksDbContext = todoTasksDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTodoTasks()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok(await todoTasksDbContext.TodoTasks
                .Where(TodoTask => TodoTask.UserId == userId)
                .ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetTodoTaskById([FromRoute] Guid id)
        {
            var todoTask = await todoTasksDbContext.TodoTasks.FindAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (todoTask == null || todoTask.UserId != userId)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoTask(TodoTask todoTask)
        {
            todoTask.Id = Guid.NewGuid();
            todoTask.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            await todoTasksDbContext.TodoTasks.AddAsync(todoTask);
            await todoTasksDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoTaskById), new { id = todoTask.Id}, todoTask);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodoTask([FromRoute] Guid id, [FromBody] TodoTask updatedTodoTask)
        {
            var existingTodoTask = await todoTasksDbContext.TodoTasks.FindAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (existingTodoTask == null || existingTodoTask.UserId != userId)
            {
                return NotFound();
            }

            existingTodoTask.Title = updatedTodoTask.Title;
            existingTodoTask.Description = updatedTodoTask.Description;
            existingTodoTask.IsCompleted = updatedTodoTask.IsCompleted;
            existingTodoTask.Index = updatedTodoTask.Index;
            existingTodoTask.Priority = updatedTodoTask.Priority;

            await todoTasksDbContext.SaveChangesAsync();

            return Ok(existingTodoTask);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteTodoTask([FromRoute] Guid id)
        {
            var existingTodoTask = await todoTasksDbContext.TodoTasks.FindAsync(id);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (existingTodoTask == null || existingTodoTask.UserId != userId)
            {
                return NotFound();
            }

            todoTasksDbContext.TodoTasks.Remove(existingTodoTask);
            await todoTasksDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
