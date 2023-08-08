namespace InsuranceINNOTech;

public interface IClaimsUnitOfWork : IBaseUnitOfWork<Claims>
{
    Task<IEnumerable<Claims>> GetAllClaimsOfTheUser(User user);
    Task<IEnumerable<Claims>> GetClaimsCreatedToday(User user);
}
