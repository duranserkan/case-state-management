using StateManagement.SharedKernel;

namespace StateManagement.Domain.Task;

public interface ITaskRepository : IRepository<TaskAggregate, long>
{
}