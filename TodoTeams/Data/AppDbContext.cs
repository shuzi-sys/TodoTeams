using Microsoft.EntityFrameworkCore;
using TodoTeams.Models;
namespace TodoTeams.Data

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet <TodoTask> Tasks { get; set; }

    }
}
