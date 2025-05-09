namespace UserManagement.Infrastructure.Configurations
{
    public class PersonalResourceConfiguration : IEntityTypeConfiguration<PersonalResource>
    {
        public void Configure(EntityTypeBuilder<PersonalResource> builder)
        {
            builder.HasKey(pr => pr.Id);

            builder.Property(pr => pr.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(pr => pr.Observations)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.OwnsOne(pr => pr.PhoneNumbers, phoneNumbers =>
            {
                phoneNumbers.Property(p => p.MobilePhone)
                    .HasMaxLength(9)
                    .IsRequired(false);

                phoneNumbers.Property(p => p.HomePhone)
                    .HasMaxLength(9)
                    .IsRequired(false);
            });

            builder.HasOne(pr => pr.User)
                .WithMany(u => u.PersonalResources)
                .HasForeignKey(pr => pr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
