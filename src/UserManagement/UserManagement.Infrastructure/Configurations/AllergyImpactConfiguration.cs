namespace UserManagement.Infrastructure.Configurations;
public class AllergyImpactConfiguration : IEntityTypeConfiguration<AllergyImpact>
{
    public void Configure(EntityTypeBuilder<AllergyImpact> builder)
    {
        builder.HasKey(ai => ai.Id);
        builder.Property(mi => mi.Id)
            .ValueGeneratedOnAdd();

        // Configuración de las propiedades
        builder.Property(ai => ai.Reaction)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasOne(mc => mc.MedicalInformation)
           .WithMany(mi => mi.AllergyImpacts)
           .HasForeignKey(mc => mc.MedicalInformationId)
           .OnDelete(DeleteBehavior.Cascade)
           .IsRequired();

        builder.HasOne(ai => ai.Allergy)
            .WithMany()
            .HasForeignKey(ai => ai.AllergyId)
            .OnDelete(DeleteBehavior.Restrict) // No eliminar en cascada
            .IsRequired();

        builder.HasOne(ai => ai.Severity)
            .WithMany()
            .HasForeignKey(ai => ai.SeverityId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

    }
}
