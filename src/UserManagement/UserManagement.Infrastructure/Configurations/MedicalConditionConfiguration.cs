namespace UserManagement.Infrastructure.Configurations;
public class MedicalConditionConfiguration : IEntityTypeConfiguration<MedicalCondition>
{
    public void Configure(EntityTypeBuilder<MedicalCondition> builder)
    {
        builder.HasKey(mc => mc.Id);
        builder.Property(mc => mc.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(mc => mc.MedicalInformation)
           .WithMany(mi => mi.MedicalConditions)
           .HasForeignKey(mc => mc.MedicalInformationId)
           .OnDelete(DeleteBehavior.Cascade)
           .IsRequired();

        builder.HasOne(mc => mc.Disease)
            .WithMany()
            .HasForeignKey(mc => mc.DiseaseId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(mc => mc.Status)
            .WithMany()
            .HasForeignKey(mc => mc.StatusId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
    }
}
