using Microsoft.EntityFrameworkCore;
namespace CqrsTodo
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Todo> Todoes { get; set; }
        public DbSet<TodoSummary> TodoSummaries { get; set; }

    }
}