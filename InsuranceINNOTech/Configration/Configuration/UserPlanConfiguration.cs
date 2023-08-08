namespace InsuranceINNOTech;

public class UserPlanConfiguration : IEntityTypeConfiguration<UserPlans>
{
    public void Configure(EntityTypeBuilder<UserPlans> Builder)
    {
        Builder
            .HasKey(e => new { e.userId, e.plainId });

        Builder
            .HasOne(sc => sc.user)
            .WithMany(s => s.userPlan)
            .HasForeignKey(sc => sc.userId);

        Builder
            .HasOne(sc => sc.plan)
            .WithMany(c => c.userPlan)
            .HasForeignKey(sc => sc.plainId);
    }
}
