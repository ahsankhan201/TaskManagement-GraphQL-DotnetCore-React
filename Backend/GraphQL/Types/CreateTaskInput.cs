using GraphQL.Models;

namespace GraphQL.GraphQL.Types
{
    public record CreateTaskInput(
        string Title,
        string? Description,
        Models.TaskStatus Status = Models.TaskStatus.Pending
    );
}