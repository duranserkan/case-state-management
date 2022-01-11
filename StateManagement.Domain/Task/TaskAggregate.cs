using StateManagement.Domain.Task.DomainEvents;
using StateManagement.SharedKernel;

namespace StateManagement.Domain.Task;

public class TaskAggregate : AggregateRoot<long>
{
    private TaskAggregate() { }

    public TaskAggregate(string taskName)
    {
        TaskName = taskName;
        Events.Add(new TaskCreated(EntityId));
    }

    public string TaskName { get; set; }
}