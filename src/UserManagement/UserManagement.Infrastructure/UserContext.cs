using UserManagement.Domain.AggregateModel.WorkCenterAggregate;

namespace UserManagement.Infrastructure;

public class UserContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;
    private IDbContextTransaction _currentTransaction;

    public UserContext(DbContextOptions<UserContext> options, IMediator mediator)
        : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    public bool HasActiveTransaction => _currentTransaction != null;
    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public DbSet<User> User { get; set; }
    public DbSet<Identification> Identification { get; set; }
    public DbSet<IdentificationType> IdentificationType { get; set; }
    public DbSet<Sex> Sex { get; set; }
    public DbSet<CivilStatus> CivilStatus { get; set; }
    public DbSet<Language> Language { get; set; }
    public DbSet<Education> Education { get; set; }
    public DbSet<Animal> Animal { get; set; }
    public DbSet<UserAnimal> UserAnimal { get; set; }
    public DbSet<UserHistory> UserHistory { get; set; }
    public DbSet<DependencyDegree> DependencyDegree { get; set; }
    public DbSet<ServiceContract> ServiceContract { get; set; }
    public DbSet<ServiceContractStatus> ServiceContractStatus { get; set; }
    public DbSet<ServiceContractStatusReason> ServiceContractStatusReason { get; set; }
    public DbSet<ServiceContractStatusHistory> ServiceContractStatusHistory { get; set; }
    public DbSet<ServiceType> ServiceType { get; set; }
    public DbSet<Residence> Residence { get; set; }
    public DbSet<Cohabitant> Cohabitant { get; set; }
    public DbSet<CohabitantType> CohabitantType { get; set; }

    public DbSet<PreferredProfessional> PreferredProfessional { get; set; }
    public DbSet<WorkCenter> WorkCenter { get; set; }
    public DbSet<UserType> UserType {  get; set; }
    public DbSet<UserTypology> UserTypology { get; set; }
    public DbSet<CentralUnit> CentralUnits { get; set; }
    public DbSet<Peripheral> Peripheral { get; set; }
    public DbSet<ServiceContract> ServiceContracts { get; set; }
    public DbSet<ServiceContractCentralUnit> ServiceContractCentralUnits { get; set; }
    public DbSet<Key> Key { get; set; }
    public DbSet<KeyStatus> KeyStatus { get; set; }
    public DbSet<KeyHistory> KeyHistory { get; set; }
    public DbSet<ServiceContractBeneficiary> ServiceContractBeneficiary { get; set; }
    public DbSet<MedicalInformation> MedicalInformation { get; set; }
    public DbSet<MedicalCondition> MedicalCondition { get; set; }
    public DbSet<AllergyImpact> AllergyImpact { get; set; }
    public DbSet<HealthCoverage> HealthCoverage { get; set; }
    public DbSet<Medication> Medication { get; set; }
    public DbSet<AreaLevel> AreaLevel { get; set; }
    public DbSet<Area> Area { get; set; }
    public DbSet<WorkCenterResource> WorkCenterResource { get; set; }
    public DbSet<PersonalResource> PersonalResource { get; set; }
    public DbSet<Resource> Resource { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
        modelBuilder.UseIntegrationEventLogs();
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return null;

        _currentTransaction = await Database.BeginTransactionAsync();
        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction == null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException("Transaction mismatch.");

        try
        {
            await SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            RollbackTransaction();
            throw;
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            _currentTransaction?.Dispose();
            _currentTransaction = null;
        }
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch domain events before saving
        await DispatchDomainEventsAsync();

        // Save changes to the database
        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task DispatchDomainEventsAsync()
    {
        var domainEntities = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity entity && entity.DomainEvents.Any())
            .Select(e => (Entity)e.Entity);

        var domainEvents = domainEntities
            .SelectMany(e => e.DomainEvents)
            .ToList();

        domainEntities.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
        {
            await _mediator.Publish(domainEvent);
        }
    }
}
