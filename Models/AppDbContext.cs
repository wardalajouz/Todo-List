using Microsoft.EntityFrameworkCore;
namespace TodoList.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Todo> Todos { get; set; }

    }
}
