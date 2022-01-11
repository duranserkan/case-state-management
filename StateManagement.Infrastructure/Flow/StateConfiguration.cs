using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StateManagement.Domain.Flow;

namespace StateManagement.Infrastructure.Flow
{
    internal class StateConfiguration:IEntityTypeConfiguration<StateEntity>
    {
        public void Configure(EntityTypeBuilder<StateEntity> builder)
        {
            builder.HasKey(stateEntity => stateEntity.Id);
            builder.Property(stateEntity => stateEntity.Name).IsRequired();
        }
    }
}
