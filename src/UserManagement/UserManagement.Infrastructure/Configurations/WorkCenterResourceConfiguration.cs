namespace UserManagement.Infrastructure.Configurations
{
    public class WorkCenterResourceConfiguration : IEntityTypeConfiguration<WorkCenterResource>
    {
        public void Configure(EntityTypeBuilder<WorkCenterResource> builder)
        {
            builder.HasKey(wr => wr.Id);

            builder.Property(wr => wr.Observations)
                .IsRequired(false)
                .HasMaxLength(200);

            builder.HasOne(wr => wr.User)
                .WithMany(wc => wc.WorkCenterResources)
                .HasForeignKey(wr => wr.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(wr => wr.Resource)
                .WithMany()
                .HasForeignKey(wr => wr.ResourceId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
