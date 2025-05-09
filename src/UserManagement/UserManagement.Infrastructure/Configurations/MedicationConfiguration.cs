namespace UserManagement.Infrastructure.Configurations;
public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(mi => mi.Id)
            .ValueGeneratedOnAdd();

        // Configuración de las propiedades
        builder.Property(m => m.Dosage)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(m => m.Recurrence)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasOne(mc => mc.MedicalInformation)
           .WithMany(mi => mi.Medications)
           .HasForeignKey(mc => mc.MedicalInformationId)
           .OnDelete(DeleteBehavior.Cascade)
           .IsRequired();

        builder.HasOne(m => m.Medicine)
            .WithMany()
            .HasForeignKey(m => m.MedicineId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
