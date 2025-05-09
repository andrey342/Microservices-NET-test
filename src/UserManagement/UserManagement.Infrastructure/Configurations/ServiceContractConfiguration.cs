using UserManagement.Domain.AggregatesModel.ServiceContractAggregate;

namespace UserManagement.Infrastructure.Configurations;

public class ServiceContractConfiguration : IEntityTypeConfiguration<ServiceContract>
{
    public void Configure(EntityTypeBuilder<ServiceContract> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.HasOne(c => c.User)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.WorkCenter)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.WorkCenterId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Residences)
            .WithOne(r => r.ServiceContract)
            .HasForeignKey(r => r.ServiceContractId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.CurrentStatus)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.CurrentStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.ServiceType)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.UserType)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.UserTypeId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.UserTypology)
            .WithMany()
            .IsRequired()
            .HasForeignKey(c => c.UserTypologyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
