namespace InsuranceINNOTech;

    public class ClaimsConfiguration : BaseConfigration<Claims>
    {
    public override void Configure(EntityTypeBuilder<Claims> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.DateCreatedAt).HasColumnType("datetime").IsRequired();
        builder.Property(e=> e.Expenses).IsRequired();
    }
}

