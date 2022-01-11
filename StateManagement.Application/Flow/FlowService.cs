using StateManagement.Application.Flow.Models;
using StateManagement.Contract.Flow.Requests;
using StateManagement.Domain.Flow;

namespace StateManagement.Application.Flow;

public interface IFlowService
{
    Task<FlowModel> GetFlowAsync(long id);
    Task<FlowModel> CreateFlowAsync(PostFlowRequest request);
    Task<FlowModel> UpdateFlowAsync(PatchFlowRequest request);
    System.Threading.Tasks.Task DeleteFlowAsync(long id);
}

public class FlowService : IFlowService
{
    private readonly IFlowRepository _flowRepository;

    public FlowService(IFlowRepository flowRepository)
    {
        _flowRepository = flowRepository;
    }

    public async Task<FlowModel> GetFlowAsync(long id)
    {
        var flowAggregate = await _flowRepository.GetByIdAsync(id);

        return new FlowModel(flowAggregate);
    }

    public async Task<FlowModel> CreateFlowAsync(PostFlowRequest request)
    {
        var flowAggregate = new FlowAggregate(request.Name);

        _flowRepository.Add(flowAggregate);

        await _flowRepository.SaveChangesAsync();

        return new FlowModel(flowAggregate);
    }

    public async Task<FlowModel> UpdateFlowAsync(PatchFlowRequest request)
    {
        var flowAggregate = await _flowRepository.GetByIdAsync(request.Id);

        //todo update states

        await _flowRepository.SaveChangesAsync();

        return new FlowModel(flowAggregate);
    }

    public async System.Threading.Tasks.Task DeleteFlowAsync(long id)
    {
        var flowAggregate = await _flowRepository.GetByIdAsync(id);

        _flowRepository.Delete(flowAggregate);
        await _flowRepository.SaveChangesAsync();
    }
}