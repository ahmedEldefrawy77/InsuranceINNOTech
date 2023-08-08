namespace InsuranceINNOTech;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfiguration(new UserConfigration())
                           .ApplyConfiguration(new HospitalPlansConfiguration())
                           .ApplyConfiguration(new HospitalConfiguration())
                           .ApplyConfiguration(new RefreshTokenConfiguration())
                           .ApplyConfiguration(new UserPlanConfiguration())
                           .ApplyConfiguration(new DependentConfiguration())
                           .ApplyConfiguration(new ClaimsConfiguration());


}
