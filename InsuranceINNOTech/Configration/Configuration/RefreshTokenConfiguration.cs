namespace InsuranceINNOTech;

public class RefreshTokenConfiguration : BaseConfigration<RefreshToken>
{
    public override void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        base.Configure(builder);

        builder
            .Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(128);
        builder
            .Property(e => e.DateCreatedAt)
            .IsRequired();
        builder
            .Property(e => e.DateExAt)
            .IsRequired();
        builder
            .Property(e => e.DateCreatedAt)
            .HasDefaultValue(DateTime.UtcNow)
            .ValueGeneratedOnAdd();
    }
}
