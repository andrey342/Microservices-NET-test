namespace UserManagement.Infrastructure.Configurations;
public class UserTypologyConfiguration : IEntityTypeConfiguration<UserTypology>
{
    public void Configure(EntityTypeBuilder<UserTypology> builder)
    {
        builder.HasKey(ua => ua.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedNever();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.WorkCenterId)
            .IsRequired();
    }
}
