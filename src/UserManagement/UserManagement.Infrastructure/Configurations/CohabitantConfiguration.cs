namespace UserManagement.Infrastructure.Configurations;

public class CohabitantConfiguration : IEntityTypeConfiguration<Cohabitant>
{
    public void Configure(EntityTypeBuilder<Cohabitant> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(c => c.Residence)
            .WithMany(r => r.Cohabitants)
            .HasForeignKey(c => c.ResidenceId)
            .OnDelete(DeleteBehavior.Cascade); // Si se elimina la residencia, se eliminan los convivientes

        builder.HasOne(c => c.CohabitantType)
            .WithMany()
            .HasForeignKey(c => c.CohabitantTypeId)
            .OnDelete(DeleteBehavior.Restrict); // No eliminar si el tipo de conviviente sigue en uso

        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Surname1)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Surname2)
            .HasMaxLength(50);

        builder.OwnsOne(u => u.PhoneNumbers, phoneNumbers =>
        {
            phoneNumbers.Property(p => p.MobilePhone)
                .HasMaxLength(9)
                .IsRequired(false); // Opcional

            phoneNumbers.Ignore(p => p.HomePhone);   // Ignorar otros campos
        });

        builder.Property(c => c.Observation)
            .HasMaxLength(500);

        builder.Property(c => c.Resource)
            .HasDefaultValue(false);
    }
}