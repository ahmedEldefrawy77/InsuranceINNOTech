namespace InsuranceINNOTech;

public class BaseSettingController<TEntity> : BaseController<TEntity> where TEntity : BaseSettingEntity
{
    private readonly IBaseSettingUnitOfWork<TEntity> _settingUnitOfWork;
    public BaseSettingController(IBaseSettingUnitOfWork<TEntity> settingUnitOfWork) : base(settingUnitOfWork) => _settingUnitOfWork = settingUnitOfWork;


    public async Task<IEnumerable<TEntity>> Search([FromRoute] string searchText) => await _settingUnitOfWork.Search(searchText);

}
