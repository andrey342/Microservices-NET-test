namespace UserManagement.Infrastructure.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasKey(al => al.Id);
            
            builder.Property(al => al.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.OwnsOne(r => r.PhoneNumbers, phoneNumbers =>
            {
                phoneNumbers.Property(p => p.MobilePhone)
                    .HasMaxLength(9)
                    .IsRequired(false);

                phoneNumbers.Property(p => p.HomePhone)
                    .HasMaxLength(9)
                    .IsRequired(false);
            });

            builder.Property(al => al.WorkCenterId)
                .IsRequired();
        }
    }
}
