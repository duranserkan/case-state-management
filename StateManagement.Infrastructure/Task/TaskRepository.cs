using StateManagement.Domain.Task;

namespace StateManagement.Infrastructure.Task;

public class TaskRepository : RepositoryBase<TaskAggregate, long>, ITaskRepository
{
    public TaskRepository(StateManagementDbContext dbContext) : base(dbContext)
    {
    }
}