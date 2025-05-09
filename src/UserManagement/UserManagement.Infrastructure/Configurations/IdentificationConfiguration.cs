namespace UserManagement.Infrastructure.Configurations;
public class IdentificationConfiguration : IEntityTypeConfiguration<Identification>
{
    public void Configure(EntityTypeBuilder<Identification> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(i => i.Number)
            .IsRequired()
            .HasMaxLength(9);

        builder.HasIndex(i => i.Number)
            .IsUnique();

        builder.HasOne(i => i.IdentificationType)
            .WithMany()
            .HasForeignKey(i => i.TypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
