namespace UserManagement.Infrastructure.Configurations;

public class PeripheralConfiguration
{
    public void Configure(EntityTypeBuilder<Peripheral> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        
        // Configurar las propiedades
        builder.Property(p => p.Code)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(p => p.SerialNumber)
            .IsRequired()
            .HasMaxLength(50);

        // Configurar la relación con ServiceContractCentralUnit
        builder.HasOne(p => p.ServiceContractCentralUnit)
            .WithMany(sccu => sccu.Peripherals)
            .HasForeignKey(p => p.ServiceContractCentralUnitId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}
