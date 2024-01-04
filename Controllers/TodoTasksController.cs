using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todo_list_backend.Data;
using todo_list_backend.Models.Entities;

namespace todo_list_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            return Ok(await todoTasksDbContext.TodoTasks.ToListAsync());
        }

        [HttpGet]
        [Route("{id:Guid}")]
        [ActionName("GetTodoTaskById")]
        public async Task<IActionResult> GetTodoTaskById([FromRoute] Guid id)
        {
            var todoTask = await todoTasksDbContext.TodoTasks.FindAsync(id);

            if (todoTask == null)
            {
                return NotFound();
            }

            return Ok(todoTask);
        }

        [HttpPost]
        public async Task<IActionResult> AddTodoTask(TodoTask todoTask)
        {
            todoTask.Id = Guid.NewGuid();
            await todoTasksDbContext.TodoTasks.AddAsync(todoTask);
            await todoTasksDbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoTaskById), new { id = todoTask.Id}, todoTask);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateTodoTask([FromRoute] Guid id, [FromBody] TodoTask updatedTodoTask)
        {
            var existingTodoTask = await todoTasksDbContext.TodoTasks.FindAsync(id);

            if (existingTodoTask == null)
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

            if (existingTodoTask == null)
            {
                return NotFound();
            }

            todoTasksDbContext.TodoTasks.Remove(existingTodoTask);
            await todoTasksDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
