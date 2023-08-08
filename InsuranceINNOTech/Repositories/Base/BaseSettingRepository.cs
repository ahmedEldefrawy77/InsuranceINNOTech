namespace InsuranceINNOTech;

public class BaseSettingRepository<TEntity> : BaseRepository<TEntity>, IBaseSettingRepository<TEntity> where TEntity : BaseSettingEntity
{
    public BaseSettingRepository(ApplicationDbContext context) : base(context) { }
    public async Task<IEnumerable<TEntity>> Search(string searchText) => await Task.Run(() => _entitySet.Where(e => e.Name!.Contains(searchText)));
}
