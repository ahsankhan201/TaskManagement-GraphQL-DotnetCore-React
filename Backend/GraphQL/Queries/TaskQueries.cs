using GraphQL.Models;
using GraphQL.Services;

namespace GraphQL.GraphQL.Queries
{
    public class TaskQueries
    {
        public async Task<IEnumerable<Models.Task>> GetAllTasksAsync(
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            return await taskService.GetAllTasksAsync(cancellationToken);
        }

        public async Task<Models.Task?> GetTaskByIdAsync(
            int id,
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            return await taskService.GetTaskByIdAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Models.Task>> GetTasksByStatusAsync(
            Models.TaskStatus status,
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            return await taskService.GetTasksByStatusAsync(status, cancellationToken);
        }
    }
}
