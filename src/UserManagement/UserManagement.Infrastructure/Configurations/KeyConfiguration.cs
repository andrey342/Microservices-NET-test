namespace UserManagement.Infrastructure.Configurations;

public class KeyConfiguration : IEntityTypeConfiguration<Key>
{
    public void Configure(EntityTypeBuilder<Key> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(c => c.Residence)
            .WithMany(r => r.Keys)
            .HasForeignKey(c => c.ResidenceId)
            .OnDelete(DeleteBehavior.Cascade); // Si se elimina la residencia, se eliminan las llaves

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Code)
            .HasMaxLength(20);

        builder.Property(c => c.Description)
            .HasMaxLength(200);

        builder.Property(c => c.Keys)
            .HasMaxLength(5);

        builder.HasOne(c => c.CurrentStatus)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.CurrentStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}