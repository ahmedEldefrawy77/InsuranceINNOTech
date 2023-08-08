namespace InsuranceINNOTech;

public class BaseConfigration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(e => e.Id).HasValueGenerator<GuidValueGenerator>();
    }
}
