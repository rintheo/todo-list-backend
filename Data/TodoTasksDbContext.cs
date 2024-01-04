using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using todo_list_backend.Models.Entities;

namespace todo_list_backend.Data
{
    public class TodoTasksDbContext : IdentityDbContext
    {
        public TodoTasksDbContext(DbContextOptions<TodoTasksDbContext> options) : base(options)
        {
        }

        public DbSet<TodoTask> TodoTasks { get; set; }
    }
}
