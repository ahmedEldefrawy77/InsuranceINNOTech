namespace InsuranceINNOTech;
    public class HospitalConfiguration : BaseSettingConfigration<Hospital>
    {
    public override void Configure(EntityTypeBuilder<Hospital> builder)
    {
        base.Configure(builder);

        builder
            .HasMany(e => e.dependents)
            .WithOne(e => e.hospital)
            .HasForeignKey(e => e.hospitalId);
        builder
            .HasMany(e => e.users)
            .WithOne(e => e.hospital)
            .HasForeignKey(e => e.hospitalId);
    }
}

