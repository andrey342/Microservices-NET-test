namespace UserManagement.Infrastructure.Configurations;

public class ServiceContractStatusHistoryConfiguration : IEntityTypeConfiguration<ServiceContractStatusHistory>
{
    public void Configure(EntityTypeBuilder<ServiceContractStatusHistory> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(c => c.ServiceContract)
            .WithMany(sc => sc.ServiceContractStatusHistories)
            .IsRequired()
            .HasForeignKey(c => c.ServiceContractId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ServiceContractStatus)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.ServiceContractStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ServiceContractStatusReason)
            .WithMany()
            .IsRequired(false)
            .HasForeignKey(c => c.ServiceContractStatusReasonId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
