namespace UserManagement.Infrastructure.Configurations;

public class KeyHistoryConfiguration : IEntityTypeConfiguration<KeyHistory>
{
    public void Configure(EntityTypeBuilder<KeyHistory> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(c => c.Key)
            .WithMany(sc => sc.KeyHistories)
            .IsRequired()
            .HasForeignKey(c => c.KeyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.KeyStatus)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.KeyStatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
