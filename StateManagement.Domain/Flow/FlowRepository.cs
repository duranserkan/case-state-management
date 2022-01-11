using StateManagement.SharedKernel;

namespace StateManagement.Domain.Flow;

public interface IFlowRepository : IRepository<FlowAggregate, long>
{
}