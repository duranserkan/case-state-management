using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StateManagement.Domain.Task;

namespace StateManagement.Infrastructure.Task;

public class TaskConfiguration : IEntityTypeConfiguration<TaskAggregate>
{
    public void Configure(EntityTypeBuilder<TaskAggregate> builder)
    {
        builder.HasKey(taskAggregate => taskAggregate.Id);
        builder.Property(taskAggregate => taskAggregate.TaskName).IsRequired();
    }
}