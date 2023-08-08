namespace InsuranceINNOTech;

public class BaseSettingUnitOfWork<TEntity> : BaseUnitOfWork<TEntity>
  , IBaseSettingUnitOfWork<TEntity> where TEntity : BaseSettingEntity
{
    private readonly IBaseSettingRepository<TEntity> _baseRepositiorySettings;
    public BaseSettingUnitOfWork(IBaseSettingRepository<TEntity> repository,
                                ILogger<BaseSettingUnitOfWork<TEntity>> logger)
                                : base(repository, logger) =>
                                  _baseRepositiorySettings = repository;

    public async Task<IEnumerable<TEntity>> Search(string searchText) => await _baseRepositiorySettings.Search(searchText);

}
