using StateManagement.Domain.Flow;

namespace StateManagement.Infrastructure.Flow;

public class FlowRepository : RepositoryBase<FlowAggregate, long>, IFlowRepository
{
    public FlowRepository(StateManagementDbContext dbContext) : base(dbContext)
    {
    }
}