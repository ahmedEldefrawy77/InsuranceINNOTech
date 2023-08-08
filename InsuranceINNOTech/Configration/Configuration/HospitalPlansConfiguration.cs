namespace InsuranceINNOTech;

public class HospitalPlansConfiguration : IEntityTypeConfiguration<HospitalPlan>
{
    public void Configure(EntityTypeBuilder<HospitalPlan> builder)
    {
        builder
            .HasKey(e => new { e.HospitalId, e.planId });

        builder
            .HasOne(e => e.hospital)
            .WithMany(e => e.hospitalPlans)
            .HasForeignKey(e => e.HospitalId);

        builder
            .HasOne(e => e.plans)
            .WithMany(e => e.hospitalPlans)
            .HasForeignKey(e => e.planId);
    }
}
