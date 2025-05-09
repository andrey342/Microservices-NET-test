namespace UserManagement.Infrastructure.Configurations;
public class HealthCoverageConfiguration : IEntityTypeConfiguration<HealthCoverage>
{
    public void Configure(EntityTypeBuilder<HealthCoverage> builder)
    {
        // Configuración de la clave primaria
        builder.HasKey(hc => hc.Id);

        builder.Property(hc => hc.Id)
            .ValueGeneratedOnAdd();

        // Configuración de las propiedades
        builder.Property(hc => hc.Provider)
            .HasMaxLength(100)
            .IsRequired(); // Obligatorio

        builder.Property(hc => hc.PolicyNumber)
            .HasMaxLength(50)
            .IsRequired(); // Obligatorio

        builder.Property(hc => hc.CoverageType)
            .HasMaxLength(50)
            .IsRequired(); // Obligatorio

        builder.Property(hc => hc.StartDate)
            .IsRequired(); // Obligatorio

        builder.Property(hc => hc.EndDate)
            .IsRequired(false); // Opcional

        // Configuración de la relación con MedicalInformation
        builder.HasOne(hc => hc.MedicalInformation)
            .WithMany(mi => mi.HealthCoverages) // Relación uno a muchos
            .HasForeignKey(hc => hc.MedicalInformationId)
            .OnDelete(DeleteBehavior.Cascade) // Eliminar en cascada
            .IsRequired();
    }
}
