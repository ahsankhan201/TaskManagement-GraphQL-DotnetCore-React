using Microsoft.EntityFrameworkCore;
using GraphQL.Data;
using GraphQL.Models;
using GraphQL.GraphQL.Types;

namespace GraphQL.Services
{
    public class TaskService : ITaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Models.Task?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByStatusAsync(Models.TaskStatus status, CancellationToken cancellationToken = default)
        {
            return await _context.Tasks
                .Where(t => t.Status == status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Models.Task> CreateTaskAsync(CreateTaskInput input, CancellationToken cancellationToken = default)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrWhiteSpace(input.Title))
                throw new ArgumentException("Title is required", nameof(input));

            var task = new Models.Task
            {
                Title = input.Title.Trim(),
                Description = input.Description?.Trim(),
                Status = input.Status,
                CreatedAt = DateTime.UtcNow
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync(cancellationToken);

            return task;
        }

        public async Task<Models.Task> UpdateTaskStatusAsync(UpdateTaskStatusInput input, CancellationToken cancellationToken = default)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == input.Id, cancellationToken);

            if (task == null)
                throw new InvalidOperationException($"Task with ID {input.Id} not found.");

            task.Status = input.Status;
            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return task;
        }

        public async Task<Models.Task> UpdateTaskAsync(int id, string? title, string? description, Models.TaskStatus? status, CancellationToken cancellationToken = default)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (task == null)
                throw new InvalidOperationException($"Task with ID {id} not found.");

            if (!string.IsNullOrWhiteSpace(title))
                task.Title = title.Trim();

            if (description != null)
                task.Description = description.Trim();

            if (status.HasValue)
                task.Status = status.Value;

            task.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return task;
        }

        public async Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default)
        {
            var task = await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);

            if (task == null)
                return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
