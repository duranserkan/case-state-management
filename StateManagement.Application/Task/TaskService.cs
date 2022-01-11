using StateManagement.Application.Task.Models;
using StateManagement.Contract.Task.Requests;
using StateManagement.Domain.Task;

namespace StateManagement.Application.Task;

public interface ITaskService
{
    Task<TaskModel> GetTaskAsync(long id);
    Task<TaskModel> CreateTaskAsync(PostTaskRequest request);
    Task<TaskModel> UpdateTaskAsync(PatchTaskRequest request);
    System.Threading.Tasks.Task DeleteTaskAsync(long id);
}
public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskModel> GetTaskAsync(long id)
    {
        var taskAggregate = await _taskRepository.GetByIdAsync(id);

        return new TaskModel(taskAggregate);
    }

    public async Task<TaskModel> CreateTaskAsync(PostTaskRequest request)
    {
        var taskAggregate = new TaskAggregate(request.Name);

        _taskRepository.Add(taskAggregate);

        await _taskRepository.SaveChangesAsync();

        return new TaskModel(taskAggregate);
    }

    public async Task<TaskModel> UpdateTaskAsync(PatchTaskRequest request)
    {
        var taskAggregate = await _taskRepository.GetByIdAsync(request.Id);

        await _taskRepository.SaveChangesAsync();

        return new TaskModel(taskAggregate);
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(long id)
    {
        var taskAggregate = await _taskRepository.GetByIdAsync(id);

        _taskRepository.Delete(taskAggregate);
        await _taskRepository.SaveChangesAsync();
    }
}