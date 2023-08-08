namespace InsuranceINNOTech;

public interface IBaseSettingUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity> where TEntity : BaseSettingEntity
{
    Task<IEnumerable<TEntity>> Search(string searchText);
}
