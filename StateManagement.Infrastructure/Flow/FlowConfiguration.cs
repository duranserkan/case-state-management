using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StateManagement.Domain.Flow;

namespace StateManagement.Infrastructure.Flow;

public class FlowConfiguration : IEntityTypeConfiguration<FlowAggregate>
{
    public void Configure(EntityTypeBuilder<FlowAggregate> builder)
    {
        builder.HasKey(flowAggregate => flowAggregate.Id);
        builder.Property(flowAggregate => flowAggregate.FlowName).IsRequired();
    }
}