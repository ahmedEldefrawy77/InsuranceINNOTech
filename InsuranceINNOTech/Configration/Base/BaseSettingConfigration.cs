namespace InsuranceINNOTech;

public class BaseSettingConfigration<TEntity> : BaseConfigration<TEntity>,
IEntityTypeConfiguration<TEntity> where TEntity : BaseSettingEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(25);

    }
}
