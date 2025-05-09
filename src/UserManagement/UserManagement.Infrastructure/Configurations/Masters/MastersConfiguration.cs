namespace UserManagement.Infrastructure.Configurations.Masters;

#region User
#region IdentificationType
public class IdentificationTypeConfiguration : IEntityTypeConfiguration<IdentificationType>
{
    public void Configure(EntityTypeBuilder<IdentificationType> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(it => it.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region Animal
public class AnimalConfiguration : IEntityTypeConfiguration<Animal>
{
    public void Configure(EntityTypeBuilder<Animal> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region Sex
public class SexConfiguration : IEntityTypeConfiguration<Sex>
{
    public void Configure(EntityTypeBuilder<Sex> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(10);
    }
}
#endregion

#region CivilStatus
public class CivilStatusConfiguration : IEntityTypeConfiguration<CivilStatus>
{
    public void Configure(EntityTypeBuilder<CivilStatus> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(cs => cs.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region Language
public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(l => l.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region Education
public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region DependencyDegree
public class DependencyDegreeConfiguration : IEntityTypeConfiguration<DependencyDegree>
{
    public void Configure(EntityTypeBuilder<DependencyDegree> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(dd => dd.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(dd => dd.Description)
            .IsRequired(false)
            .HasMaxLength(500);
    }
}
#endregion

#region Disease
public class DiseaseConfiguration : IEntityTypeConfiguration<Disease>
{
    public void Configure(EntityTypeBuilder<Disease> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region MedicalConditionStatus
public class MedicalConditionStatusConfiguration : IEntityTypeConfiguration<MedicalConditionStatus>
{
    public void Configure(EntityTypeBuilder<MedicalConditionStatus> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region AllergySeverity
public class AllergySeverityConfiguration : IEntityTypeConfiguration<AllergySeverity>
{
    public void Configure(EntityTypeBuilder<AllergySeverity> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region Allergy
public class AllergyConfiguration : IEntityTypeConfiguration<Allergy>
{
    public void Configure(EntityTypeBuilder<Allergy> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region Medicine
public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
{
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion
#endregion

#region ServiceContract
#region ServiceContractStatus
public class ServiceContractStatusConfiguration : IEntityTypeConfiguration<ServiceContractStatus>
{
    public void Configure(EntityTypeBuilder<ServiceContractStatus> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();
        builder.Property(cs => cs.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(cs => cs.Default)
            .IsRequired();

        builder.HasIndex(cs => cs.Default)
            .IsUnique()
            .HasFilter("[Default] = 1");
    }
}
#endregion

#region ServiceContractStatusReason
public class ServiceContractStatusReasonConfiguration : IEntityTypeConfiguration<ServiceContractStatusReason>
{
    public void Configure(EntityTypeBuilder<ServiceContractStatusReason> builder)
    {
        builder.HasKey(sr => sr.Id);
        builder.Property(sr => sr.Id)
               .ValueGeneratedOnAdd();
        builder.Property(sr => sr.Name)
            .IsRequired(false)
            .HasMaxLength(50);
        builder.HasOne(sr => sr.ServiceContractStatus)
            .WithMany()
            .IsRequired()
            .HasForeignKey(sr => sr.ServiceContractStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

#endregion

#region KeyStatus
public class KeyStatusConfiguration : IEntityTypeConfiguration<KeyStatus>
{
    public void Configure(EntityTypeBuilder<KeyStatus> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(ks => ks.Name)
            .IsRequired()
            .HasMaxLength(50);
        
        builder.Property(cs => cs.Default)
            .IsRequired();
        
        builder.HasIndex(cs => cs.Default)
            .IsUnique()
            .HasFilter("[Default] = 1");
    }
}
#endregion

#region ServiceType
public class ServiceTypeConfiguration : IEntityTypeConfiguration<ServiceType>
{
    public void Configure(EntityTypeBuilder<ServiceType> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();
        builder.Property(st => st.Name)
            .IsRequired()
            .HasMaxLength(50);
    }
}
#endregion

#region CohabitationType
public class CohabitantTypeConfiguration : IEntityTypeConfiguration<CohabitantType>
{
    public void Configure(EntityTypeBuilder<CohabitantType> builder)
    {
        builder.HasKey(ct => ct.Id);
        builder.Property(ct => ct.Id)
            .ValueGeneratedOnAdd();

        builder.Property(ct => ct.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(ct => ct.Name)
            .IsUnique(); // Evita nombres duplicados en la tabla de tipos de convivientes
    }
}
#endregion

#endregion
