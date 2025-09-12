using Microsoft.EntityFrameworkCore;
using Xunit;
using GraphQL.Data;
using GraphQL.Services;
using GraphQL.Models;
using GraphQL.GraphQL.Types;
using ST = System.Threading.Tasks;
using TaskStatus = GraphQL.Models.TaskStatus;

namespace GraphQL.Tests.Services
{
    public class TaskServiceTests : IDisposable
    {
        private readonly TaskDbContext _context;
        private readonly TaskService _taskService;

        public TaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TaskDbContext(options);
            _taskService = new TaskService(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public async ST.Task CreateTaskAsync_WithValidInput_ShouldCreateTask()
        {
            // Arrange
            var input = new CreateTaskInput("Test Task", "Test Description", TaskStatus.Pending);

            // Act
            var result = await _taskService.CreateTaskAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal("Test Task", result.Title);
            Assert.Equal("Test Description", result.Description);
            Assert.Equal(Models.TaskStatus.Pending, result.Status);
            Assert.True(result.CreatedAt > DateTime.MinValue);
        }

        [Fact]
        public async ST.Task CreateTaskAsync_WithNullInput_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _taskService.CreateTaskAsync(null!));
        }

        [Fact]
        public async ST.Task CreateTaskAsync_WithEmptyTitle_ShouldThrowArgumentException()
        {
            // Arrange
            var input = new CreateTaskInput("", "Test Description", TaskStatus.Pending);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.CreateTaskAsync(input));
        }

        [Fact]
        public async ST.Task CreateTaskAsync_WithWhitespaceTitle_ShouldThrowArgumentException()
        {
            // Arrange
            var input = new CreateTaskInput("   ", "Test Description", TaskStatus.Pending);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.CreateTaskAsync(input));
        }

        [Fact]
        public async ST.Task GetAllTasksAsync_WithNoTasks_ShouldReturnEmptyList()
        {
            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public async ST.Task GetAllTasksAsync_WithTasks_ShouldReturnTasksOrderedByCreatedAtDescending()
        {
            // Arrange
            var task1 = new Models.Task { Title = "Task 1", CreatedAt = DateTime.UtcNow.AddMinutes(-10) };
            var task2 = new Models.Task { Title = "Task 2", CreatedAt = DateTime.UtcNow.AddMinutes(-5) };
            var task3 = new Models.Task { Title = "Task 3", CreatedAt = DateTime.UtcNow };

            _context.Tasks.AddRange(task1, task2, task3);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            var tasks = result.ToList();
            Assert.Equal(3, tasks.Count);
            Assert.Equal("Task 3", tasks[0].Title);
            Assert.Equal("Task 2", tasks[1].Title);
            Assert.Equal("Task 1", tasks[2].Title);
        }

        [Fact]
        public async ST.Task GetTaskByIdAsync_WithExistingId_ShouldReturnTask()
        {
            // Arrange
            var task = new Models.Task { Title = "Test Task", Description = "Test Description" };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskService.GetTaskByIdAsync(task.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(task.Id, result.Id);
            Assert.Equal("Test Task", result.Title);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public async ST.Task GetTaskByIdAsync_WithNonExistingId_ShouldReturnNull()
        {
            // Act
            var result = await _taskService.GetTaskByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async ST.Task GetTasksByStatusAsync_WithMatchingStatus_ShouldReturnFilteredTasks()
        {
            // Arrange
            var task1 = new Models.Task { Title = "Task 1", Status = TaskStatus.Pending, CreatedAt = DateTime.UtcNow.AddMinutes(-5) };
            var task2 = new Models.Task { Title = "Task 2", Status = TaskStatus.Completed, CreatedAt = DateTime.UtcNow.AddMinutes(-3) };
            var task3 = new Models.Task { Title = "Task 3", Status = TaskStatus.Pending, CreatedAt = DateTime.UtcNow };

            _context.Tasks.AddRange(task1, task2, task3);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskService.GetTasksByStatusAsync(TaskStatus.Pending);

            // Assert
            var tasks = result.ToList();
            Assert.Equal(2, tasks.Count);
            Assert.All(tasks, t => Assert.Equal(TaskStatus.Pending, t.Status));
            Assert.Equal("Task 3", tasks[0].Title); // Should be ordered by CreatedAt descending
            Assert.Equal("Task 1", tasks[1].Title);
        }

        [Fact]
        public async ST.Task UpdateTaskStatusAsync_WithValidInput_ShouldUpdateTaskStatus()
        {
            // Arrange
            var task = new Models.Task { Title = "Test Task", Status = TaskStatus.Pending };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            var input = new UpdateTaskStatusInput(task.Id, TaskStatus.InProgress);

            // Act
            var result = await _taskService.UpdateTaskStatusAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(task.Id, result.Id);
            Assert.Equal(TaskStatus.InProgress, result.Status);
            Assert.NotNull(result.UpdatedAt);
            Assert.True(result.UpdatedAt > task.CreatedAt);
        }

        [Fact]
        public async ST.Task UpdateTaskStatusAsync_WithNullInput_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _taskService.UpdateTaskStatusAsync(null!));
        }

        [Fact]
        public async ST.Task UpdateTaskStatusAsync_WithNonExistingId_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var input = new UpdateTaskStatusInput(999, TaskStatus.Completed);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _taskService.UpdateTaskStatusAsync(input));
        }

        [Fact]
        public async ST.Task UpdateTaskAsync_WithValidData_ShouldUpdateTask()
        {
            // Arrange
            var task = new Models.Task
            {
                Title = "Original Title",
                Description = "Original Description",
                Status = TaskStatus.Pending
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskService.UpdateTaskAsync(
                task.Id,
                "Updated Title",
                "Updated Description",
                TaskStatus.InProgress);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Updated Description", result.Description);
            Assert.Equal(TaskStatus.InProgress, result.Status);
            Assert.NotNull(result.UpdatedAt);
        }

        [Fact]
        public async ST.Task UpdateTaskAsync_WithNonExistingId_ShouldThrowInvalidOperationException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _taskService.UpdateTaskAsync(999, "New Title", null, null));
        }

        [Fact]
        public async ST.Task UpdateTaskAsync_WithPartialUpdate_ShouldUpdateOnlySpecifiedFields()
        {
            // Arrange
            var task = new Models.Task
            {
                Title = "Original Title",
                Description = "Original Description",
                Status = TaskStatus.Pending
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act - Only update title
            var result = await _taskService.UpdateTaskAsync(task.Id, "Updated Title", null, null);

            // Assert
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Original Description", result.Description); // Should remain unchanged
            Assert.Equal(TaskStatus.Pending, result.Status); // Should remain unchanged
        }

        [Fact]
        public async ST.Task DeleteTaskAsync_WithExistingId_ShouldDeleteTaskAndReturnTrue()
        {
            // Arrange
            var task = new Models.Task { Title = "Test Task" };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskService.DeleteTaskAsync(task.Id);

            // Assert
            Assert.True(result);

            // Verify task is deleted
            var deletedTask = await _context.Tasks.FindAsync(task.Id);
            Assert.Null(deletedTask);
        }

        [Fact]
        public async ST.Task DeleteTaskAsync_WithNonExistingId_ShouldReturnFalse()
        {
            // Act
            var result = await _taskService.DeleteTaskAsync(999);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async ST.Task CreateTaskAsync_ShouldTrimWhitespaceFromTitle()
        {
            // Arrange
            var input = new CreateTaskInput("  Test Task  ", "  Test Description  ");

            // Act
            var result = await _taskService.CreateTaskAsync(input);

            // Assert
            Assert.Equal("Test Task", result.Title);
            Assert.Equal("Test Description", result.Description);
        }

        [Fact]
        public async ST.Task UpdateTaskAsync_ShouldTrimWhitespaceFromFields()
        {
            // Arrange
            var task = new Models.Task { Title = "Original Title", Description = "Original Description" };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            // Act
            var result = await _taskService.UpdateTaskAsync(
                task.Id,
                "  Updated Title  ",
                "  Updated Description  ",
                null);

            // Assert
            Assert.Equal("Updated Title", result.Title);
            Assert.Equal("Updated Description", result.Description);
        }
    }
}