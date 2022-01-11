using StateManagement.Domain.Task;

namespace StateManagement.Application.Task.Models;

public class TaskModel
{
    public TaskModel(TaskAggregate taskAggregate)
    {
        Name = taskAggregate.TaskName;
        Id = taskAggregate.Id;
    }

    public string Name { get; }
    public long Id { get; }
}