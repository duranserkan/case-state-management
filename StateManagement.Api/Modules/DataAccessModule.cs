using Microsoft.EntityFrameworkCore;
using StateManagement.Domain.Flow;
using StateManagement.Domain.Task;
using StateManagement.Infrastructure;
using StateManagement.Infrastructure.Flow;
using StateManagement.Infrastructure.Task;

namespace StateManagement.Api.Modules;

public static class DataAccessModule
{
    public static void AddDataAccess(this IServiceCollection services, IConfiguration config)
    {
        var testConnString = config.GetConnectionString("stateManagement");

        services.AddDbContext<StateManagementDbContext>(dbContextOptionsBuilder =>
        {
            dbContextOptionsBuilder.UseNpgsql(testConnString);
        });

        services.AddScoped<IFlowRepository, FlowRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
    }
}