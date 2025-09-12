using GraphQL.Models;
using GraphQL.GraphQL.Types;

namespace GraphQL.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Models.Task>> GetAllTasksAsync(CancellationToken cancellationToken = default);
        Task<Models.Task?> GetTaskByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Models.Task>> GetTasksByStatusAsync(Models.TaskStatus status, CancellationToken cancellationToken = default);
        Task<Models.Task> CreateTaskAsync(CreateTaskInput input, CancellationToken cancellationToken = default);
        Task<Models.Task> UpdateTaskStatusAsync(UpdateTaskStatusInput input, CancellationToken cancellationToken = default);
        Task<Models.Task> UpdateTaskAsync(int id, string? title, string? description, Models.TaskStatus? status, CancellationToken cancellationToken = default);
        Task<bool> DeleteTaskAsync(int id, CancellationToken cancellationToken = default);
    }
}
