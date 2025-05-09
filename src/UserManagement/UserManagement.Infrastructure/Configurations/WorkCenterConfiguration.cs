namespace UserManagement.Infrastructure.Configurations;
public class WorkCenterConfiguration : IEntityTypeConfiguration<WorkCenter>
{
    public void Configure(EntityTypeBuilder<WorkCenter> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

    }
}
