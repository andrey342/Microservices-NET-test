namespace UserManagement.Infrastructure.Configurations;
public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(u => u.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Surname1)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.Surname2)
            .HasMaxLength(50);

        builder.Property(u => u.Appellative)
            .HasMaxLength(50);

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                 .HasColumnName("Email")
                 .HasMaxLength(100);
        });

        builder.Property(u => u.Birthdate)
            .IsRequired();

        builder.Property(u => u.CongratulateOnBirthDate)
            .HasDefaultValue(false);

        builder.OwnsOne(u => u.PhoneNumbers);

        builder.OwnsOne(u => u.CallTime, callTime =>
        {
            callTime.Property(ct => ct.Value)
                .HasColumnName("CallTime");
        });

        builder.Property(u => u.Observation)
            .HasMaxLength(500);

        builder.HasOne(u => u.Sex)
            .WithMany()
            .IsRequired()
            .HasForeignKey(u => u.SexId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.Identification)
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(u => u.IdentificationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(u => u.CivilStatus)
            .WithMany()
            .IsRequired()
            .HasForeignKey(u => u.CivilStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relaciones con claves foráneas que son opcionales y no se eliminan en cascada
        builder.HasOne(u => u.Language)
            .WithMany()
            .HasForeignKey(u => u.LanguageId)
            .OnDelete(DeleteBehavior.Restrict) // No borrar en cascada
            .IsRequired(false); // Nullable

        builder.HasOne(u => u.Education)
            .WithMany()
            .HasForeignKey(u => u.EducationId)
            .OnDelete(DeleteBehavior.Restrict) // No borrar en cascada
            .IsRequired(false); // Nullable

        builder.HasOne(u => u.MedicalInformation)
            .WithMany()
            .HasForeignKey(u => u.MedicalInformationId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false); // Nullable

        builder.HasMany(u => u.UserAnimals)
            .WithOne()
            .HasForeignKey(ua => ua.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.PreferredProfessional)
            .WithMany()
            .HasForeignKey(u => u.PreferredProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.UserHistories)
            .WithOne()
            .HasForeignKey(uh => uh.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.PersonalResources)
            .WithOne(pr => pr.User)
            .HasForeignKey(pr => pr.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.WorkCenterResources)
            .WithOne(wr => wr.User)
            .HasForeignKey(wr => wr.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
