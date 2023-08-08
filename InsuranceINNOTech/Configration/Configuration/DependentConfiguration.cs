namespace InsuranceINNOTech;

public class DependentConfiguration : BaseSettingConfigration<Dependent>
{
    public override void Configure(EntityTypeBuilder<Dependent> builder)
    {
        base.Configure(builder);

        builder
            .Property(e=> e.Age)
            .IsRequired();
        builder
            .Property(e=> e.Telephone)
            .IsRequired()
            .HasMaxLength(15);
        builder
            .HasOne(e => e.plans)
            .WithMany(e => e.dependents)
            .HasForeignKey(e => e.PlansId);
        builder
            .HasMany(e => e.claims)
            .WithOne(e => e.dependent)
            .HasForeignKey(e => e.dependentId)
            .OnDelete(DeleteBehavior.Restrict);


    }
}
