using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.Infrastructure.Configurations
{
    public class AreaLevelConfiguration : IEntityTypeConfiguration<AreaLevel>
    {
        public void Configure(EntityTypeBuilder<AreaLevel> builder)
        {
            builder.HasKey(al => al.Id);
            builder.Property(ua => ua.Id)
                .ValueGeneratedOnAdd();

            builder.Property(al => al.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(al => al.Level)
                .IsRequired();

            builder.Property(al => al.WorkCenterId)
                .IsRequired();

            // Índice único: combinación de WorkCenterId + Level
            builder.HasIndex(al => new { al.WorkCenterId, al.Level })
                .IsUnique();
        }
    }
}
