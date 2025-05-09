using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserManagement.Infrastructure.Configurations;
public class MedicalInformationConfiguration : IEntityTypeConfiguration<MedicalInformation>
{
    public void Configure(EntityTypeBuilder<MedicalInformation> builder)
    {
        builder.HasKey(mi => mi.Id);
        builder.Property(mi => mi.Id)
            .ValueGeneratedOnAdd();

        builder.Property(mi => mi.BarthelIndex)
            .IsRequired(false);

        builder.Property(mi => mi.LawtonIndex)
            .IsRequired(false);

        builder.Property(mi => mi.PhysicalScaleBSN)
            .IsRequired(false);

        builder.Property(mi => mi.PhysicalScaleBVD)
            .IsRequired(false);

        builder.Property(mi => mi.PsychologicalScaleBSN)
            .IsRequired(false);

        builder.Property(mi => mi.PsychologicalScaleBVD)
            .IsRequired(false);

        builder.Property(mi => mi.SocialScaleBSN)
            .IsRequired(false);

        builder.Property(mi => mi.SocialScaleBVD)
            .IsRequired(false);

        builder.Property(mi => mi.Observation)
            .HasMaxLength(500)
            .IsRequired(false);

        builder.HasOne(mi => mi.DependencyDegree)
            .WithMany()
            .HasForeignKey(mi => mi.DependencyDegreeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(mi => mi.MedicalConditions)
            .WithOne(mc => mc.MedicalInformation)
            .HasForeignKey(mc => mc.MedicalInformationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(mi => mi.Medications)
            .WithOne(m => m.MedicalInformation)
            .HasForeignKey(m => m.MedicalInformationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(mi => mi.AllergyImpacts)
            .WithOne(ai => ai.MedicalInformation)
            .HasForeignKey(ai => ai.MedicalInformationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(mi => mi.HealthCoverages)
            .WithOne(hc => hc.MedicalInformation)
            .HasForeignKey(hc => hc.MedicalInformationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
