namespace UserManagement.Infrastructure.Configurations;

public class ServiceContractCentralUnitConfiguration : IEntityTypeConfiguration<ServiceContractCentralUnit>
{
    public void Configure(EntityTypeBuilder<ServiceContractCentralUnit> builder)
    {
        builder.HasKey(sccu => sccu.Id);
        builder.Property(sccu => sccu.Id)
            .ValueGeneratedOnAdd();

        // Configurar la relación con CentralUnit
        builder.HasOne(sccu => sccu.CentralUnit)
            .WithMany(cu => cu.ServiceContractCentralUnits)
            .HasForeignKey(sccu => sccu.CentralUnitId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar la relación con ServiceContract
        builder.HasOne(sccu => sccu.ServiceContract)
            .WithMany(sc => sc.ServiceContractCentralUnits)
            .HasForeignKey(sccu => sccu.ServiceContractId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar la relación con Peripheral
        builder.HasMany(sccu => sccu.Peripherals)
            .WithOne(p => p.ServiceContractCentralUnit)
            .HasForeignKey(p => p.ServiceContractCentralUnitId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configurar la restricción de unicidad
        builder.HasIndex(sccu => new { sccu.CentralUnitId, sccu.ServiceContractId })
            .IsUnique();
    }
}
