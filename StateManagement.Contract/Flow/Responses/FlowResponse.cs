namespace StateManagement.Contract.Flow.Responses;

public class FlowResponse
{
    public FlowResponse(long id)
    {
        Id = id;
    }

    public long Id { get; }
}