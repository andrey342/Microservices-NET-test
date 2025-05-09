namespace UserManagement.Infrastructure.Configurations;

public class PreferredProfessionalConfiguration : IEntityTypeConfiguration<PreferredProfessional>
{
    public void Configure(EntityTypeBuilder<PreferredProfessional> builder)
    {
        builder.HasKey(pp => pp.Id);
        builder.Property(pp => pp.Id)
            .ValueGeneratedOnAdd();

        builder.Property(pp => pp.ProfessionalId)
            .IsRequired();

        builder.HasIndex(pp => pp.ProfessionalId)
            .IsUnique();
            
        // Validaciones de longitud y restricciones
        builder.Property(pp => pp.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pp => pp.Surname1)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pp => pp.Surname2)
            .HasMaxLength(100)
            .IsRequired(false);
    }
}