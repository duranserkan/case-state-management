using Microsoft.EntityFrameworkCore;
using StateManagement.Domain.Flow;
using StateManagement.Domain.Task;

namespace StateManagement.Infrastructure
{
    public class StateManagementDbContext : DbContextBase
    {
        public StateManagementDbContext(DbContextOptions options) : base(options) { }

        public DbSet<FlowAggregate> Flows { get; set; }
        public DbSet<TaskAggregate> Tasks { get; set; }
    }
}
