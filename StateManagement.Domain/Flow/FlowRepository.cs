using StateManagement.SharedKernel;

namespace StateManagement.Domain.Flow;

public interface FlowRepository : IRepository<FlowAggregate, long>
{
}