using StateManagement.Application.Flow;
using StateManagement.Application.Task;

namespace StateManagement.Api.Modules;

public static class ApplicationModule
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IFlowService, FlowService>();
        services.AddTransient<ITaskService, TaskService>();
    }
}