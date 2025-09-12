using GraphQL.Models;

namespace GraphQL.GraphQL.Types
{
    public record UpdateTaskStatusInput(
        int Id,
        Models.TaskStatus Status
    );
}