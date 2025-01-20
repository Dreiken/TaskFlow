using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace TaskFlow.Tests
{
    using TaskFlow.Controllers;
    using TaskFlow.Data;
    using TaskFlow.Models;
    public class TasksControllerTests
    {
        private TaskDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: "TaskFlowTestDb")
                .Options;
                return new TaskDbContext(options);
        }
    

        [Fact]
        public async void GetTasks_ReturnsOkResult_WhenTasksExist()
        {
            //Arrange
            var context = GetDbContext();

            //Seed Data
            context.Tasks.Add(new Task { Title = "Test Task 1", Description = "Test Description 1", Status = 0});
            context.Tasks.Add(new Task { Title = "Test Task 1", Description = "Test Description 1", Status = 1});
            await context.SaveChangesAsync();

            //Debugging
            var tasksInDb = await context.Tasks.ToListAsync();
            Assert.NotEmpty(tasksInDb);

            var controller = new TaskController(context);

            //Act
            var result = await controller.GetTasks();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var tasks = Assert.IsAssignableFrom<IEnumerable<Task>>(actionResult.Value);
            Assert.NotEmpty(tasks); // SHouldnt be empty
            Assert.Equal(2, tasks.Count()); // Verifying the count of tasks
        }

        [Fact]
        public async void CreateTask_ReturnsCreatedReult_WhenTaskIsValid()
        {
            //Arrange
            var context = GetDbContext();
            var controller = new TaskController(context);
            var newTask = new Task { Title= "New Task", Description = "Task description", Status = 0};

            //Act
            var result = await controller.CreateTask(newTask);

            //Assert
            var actionResult = Assert.IsType<CreatedAtActionResult>(result);
            var task = Assert.IsType<Task>(actionResult.Value);
            Assert.Equal("New Task", task.Title);
        }
    }
}