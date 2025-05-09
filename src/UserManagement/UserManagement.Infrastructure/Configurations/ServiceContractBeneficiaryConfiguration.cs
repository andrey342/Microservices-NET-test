namespace UserManagement.Infrastructure.Configurations;

public class ServiceContractBeneficiaryConfiguration : IEntityTypeConfiguration<ServiceContractBeneficiary>
{
    public void Configure(EntityTypeBuilder<ServiceContractBeneficiary> builder)
    {
        builder.HasKey(scb => scb.Id);
        builder.Property(scb => scb.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(scb => scb.ServiceContract)
            .WithMany(sc => sc.Beneficiaries)
            .HasForeignKey(scb => scb.ServiceContractId);

        builder.HasOne(scb => scb.User)
            .WithMany(u => u.ServiceContractsBeneficiary)
            .HasForeignKey(scb => scb.UserId);

        builder.HasIndex(scb => new { scb.ServiceContractId, scb.UserId })
            .IsUnique();
    }
}