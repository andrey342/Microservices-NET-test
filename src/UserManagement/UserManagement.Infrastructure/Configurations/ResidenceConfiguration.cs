using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UserManagement.Infrastructure.Configurations
{
    public class ResidenceConfiguration : IEntityTypeConfiguration<Residence>
    {
        public void Configure(EntityTypeBuilder<Residence> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(r => r.ServiceContract)
                .WithMany(c => c.Residences)
                .HasForeignKey(r => r.ServiceContractId)
                .OnDelete(DeleteBehavior.Cascade);

            var provinceConverter = new ValueConverter<Province, string>(
                 v => v.Name,
                 v => new Province(v));

            var roadTypeConverter = new ValueConverter<RoadType, string>(
                 v => v.Name,
                 v => new RoadType(v));

            builder.OwnsOne(r => r.Address, address =>
            {
                address.Property(a => a.RoadType)
                    .IsRequired()
                    .HasConversion(roadTypeConverter);

                address.Property(a => a.StreetName)
                    .IsRequired()
                    .HasMaxLength(200);

                address.Property(a => a.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10);

                address.Property(a => a.Town)
                    .IsRequired()
                    .HasMaxLength(100);

                address.Property(a => a.Province)
                    .IsRequired()
                    .HasConversion(provinceConverter);

                address.Property(a => a.Door)
                    .HasMaxLength(10);

                address.Property(a => a.Floor)
                    .HasMaxLength(10);

                address.Property(a => a.Number)
                    .HasMaxLength(10);

                address.Property(a => a.Stair)
                    .HasMaxLength(10);

                address.Property(a => a.Latitude)
                    .HasColumnType("decimal(9,6)");

                address.Property(a => a.Longitude)
                    .HasColumnType("decimal(9,6)");
            });

            builder.Property(r => r.Elevator)
                .IsRequired();

            builder.Property(r => r.Concierge)
                .IsRequired();

            builder.Property(r => r.Doorman)
                .IsRequired();

            builder.Property(r => r.FireHydrant)
                .IsRequired();

            builder.Property(r => r.Wifi)
                .IsRequired();

            builder.Property(r => r.Gas)
                .IsRequired();

            builder.Property(r => r.Electricity)
                .IsRequired();

            builder.Property(r => r.Water)
                .IsRequired();

            builder.Property(r => r.Internet)
                .IsRequired();

            builder.Property(r => r.ArchitecturalBarrierEntrance)
                .HasMaxLength(100);

            builder.Property(r => r.ArchitecturalBarriereResidence)
                .HasMaxLength(100);

            builder.Property(r => r.Observation)
                .HasMaxLength(500);

            builder.Property(r => r.IsCurrentResidence)
                .IsRequired();

            // Restricción única para asegurar que solo una residencia puede ser la actual por contrato
            builder.HasIndex(r => new { r.ServiceContractId, r.IsCurrentResidence })
                .IsUnique()
                .HasFilter("[IsCurrentResidence] = 1");
        }
    }
}
