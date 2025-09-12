using HotChocolate;
using GraphQL.GraphQL.Types;
using GraphQL.Models;
using GraphQL.Services;
using TaskStatus = GraphQL.Models.TaskStatus;

namespace GraphQL.GraphQL.Mutations
{
    public class TaskMutations
    {
        public async Task<Models.Task> CreateTaskAsync(
            CreateTaskInput input,
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            try
            {
                return await taskService.CreateTaskAsync(input, cancellationToken);
            }
            catch (ArgumentException ex)
            {
                throw new GraphQLException(ex.Message);
            }
        }

        public async Task<Models.Task> UpdateTaskStatusAsync(
            UpdateTaskStatusInput input,
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            try
            {
                return await taskService.UpdateTaskStatusAsync(input, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new GraphQLException(ex.Message);
            }
        }

        public async Task<Models.Task> UpdateTaskAsync(
            int id,
            string? title,
            string? description,
            TaskStatus? status,
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            try
            {
                return await taskService.UpdateTaskAsync(id, title, description, status, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                throw new GraphQLException(ex.Message);
            }
        }

        public async Task<bool> DeleteTaskAsync(
            int id,
            ITaskService taskService,
            CancellationToken cancellationToken)
        {
            return await taskService.DeleteTaskAsync(id, cancellationToken);
        }
    }
}
