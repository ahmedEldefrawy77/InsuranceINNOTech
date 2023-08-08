namespace InsuranceINNOTech;

public interface IDependentRepository : IBaseSettingRepository<Dependent>
{
    Task<IEnumerable<Dependent>> GetDependentAsync(Guid id);
    Task DeleteDependent(Guid id , string Email);
    Task<Dependent> GetDependentByEmail(string mail);

}
