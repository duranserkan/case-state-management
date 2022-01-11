using StateManagement.Domain.Task;

namespace StateManagement.Infrastructure.Task;

public class TaskRepository : RepositoryBase<TaskAggregate, long>, ITaskRepository
{
    public TaskRepository(Microsoft.EntityFrameworkCore.DbContext dbContext) : base(dbContext)
    {
    }
}