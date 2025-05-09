namespace UserManagement.Infrastructure.Configurations;

public class CentralUnitConfiguration : IEntityTypeConfiguration<CentralUnit>
{
    public void Configure(EntityTypeBuilder<CentralUnit> builder)
    {
        builder.HasKey(cu => cu.Id);
        builder.Property(cu => cu.Id)
            .ValueGeneratedOnAdd();

        // Configurar las propiedades
        builder.Property(c => c.Code)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.SerialNumber)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Phone)
            .IsRequired()
            .HasMaxLength(15);

        // Configurar la relación con ServiceContractCentralUnit
        builder.HasMany(c => c.ServiceContractCentralUnits)
            .WithOne(sccu => sccu.CentralUnit)
            .HasForeignKey(sccu => sccu.CentralUnitId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}