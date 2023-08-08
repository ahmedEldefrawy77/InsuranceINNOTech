namespace InsuranceINNOTech;

public interface IDependentUnitOfWork : IBaseSettingUnitOfWork<Dependent>
{
    Task<IEnumerable<Dependent>> GetDependentOfUser(User user);
    Task DeleteDependent(User user ,string Email);
}
