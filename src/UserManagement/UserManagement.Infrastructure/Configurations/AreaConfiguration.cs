using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.Infrastructure.Configurations
{
    public class AreaConfiguration
    : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasOne(a => a.AreaLevel)
                .WithMany(al => al.Areas)
                .HasForeignKey(a => a.AreaLevelId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Parent)
                .WithMany(a => a.Childrens)
                .HasForeignKey(a => a.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
