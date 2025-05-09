namespace UserManagement.Infrastructure.Configurations;
public class UserAnimalConfiguration : IEntityTypeConfiguration<UserAnimal>
{
    public void Configure(EntityTypeBuilder<UserAnimal> builder)
    {
        builder.HasKey(ua => ua.Id);
        builder.Property(ua => ua.Id)
               .ValueGeneratedOnAdd();

        builder.Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(ua => ua.Animal)
            .WithMany()
            .HasForeignKey(ua => ua.AnimalId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
