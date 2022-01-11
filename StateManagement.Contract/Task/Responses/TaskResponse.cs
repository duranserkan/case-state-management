namespace StateManagement.Contract.Task.Responses;

public class TaskResponse
{
    public TaskResponse(long id)
    {
        Id = id;
    }

    public long Id { get; }
}