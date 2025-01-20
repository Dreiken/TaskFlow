namespace TaskFlow.Data{

    using Microsoft.EntityFrameworkCore; 
    using TaskFlow.Models;
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options) {}

        public DbSet<Task> Tasks {get; set;}
    }
}