using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace InsuranceINNOTech;

public interface IUserRepository : IBaseSettingRepository<User>
{
    Task<User> Get(Guid id);
    Task<IEnumerable<User>> Get();
    Task<User> SearchByMail(string mail);
    Task DeleteByMail(string mail);
    Task<User> GetbyToken(string token);

}
