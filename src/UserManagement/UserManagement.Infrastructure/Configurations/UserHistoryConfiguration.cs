namespace UserManagement.Infrastructure.Configurations
{
    public class UserHistoryConfiguration : IEntityTypeConfiguration<UserHistory>
    {
        public void Configure(EntityTypeBuilder<UserHistory> builder)
        {
            builder.HasKey(uh => uh.Id);
            builder.Property(ua => ua.Id)
                .ValueGeneratedOnAdd();
            builder.Property(uh => uh.Type).IsRequired().HasMaxLength(100);
            builder.Property(uh => uh.Action).IsRequired().HasMaxLength(100);
            builder.Property(uh => uh.Description).IsRequired().HasMaxLength(500);
            builder.Property(uh => uh.UserId).IsRequired();
            builder.Property(uh => uh.ServiceContractId).IsRequired(false);
            builder.Property(uh => uh.OccurredOn).IsRequired();
        }
    }
}
