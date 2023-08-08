namespace InsuranceINNOTech;

public class UserConfigration : BaseSettingConfigration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Mail).IsRequired();
        builder.HasIndex(e => e.Mail).IsUnique();

        builder
            .Property(e => e.Role)
            .IsRequired()
            .HasMaxLength(5)
            .HasDefaultValue("User")
            .ValueGeneratedOnAdd();
        builder
            .Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(int.MaxValue);
        builder
            .HasMany(e=> e.Dependents)
            .WithOne(e=> e.user)
            .HasForeignKey(e =>e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(e => e.RefreshToken)
            .WithOne(e => e.user)
            .HasForeignKey<RefreshToken>(e => e.UserId);
        builder
            .HasMany(e => e.claims)
            .WithOne(e => e.user)
            .HasForeignKey(e => e.userId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.
            Property(e => e.Age).IsRequired()
            .HasMaxLength(3);
        builder
            .Property(e=> e.Gender).IsRequired()
            .HasDefaultValue("UnKnown")
            .ValueGeneratedOnAdd();

    }
}