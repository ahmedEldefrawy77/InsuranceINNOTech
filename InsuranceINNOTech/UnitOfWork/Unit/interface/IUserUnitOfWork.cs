namespace InsuranceINNOTech;

public interface IUserUnitOfWork : IBaseSettingUnitOfWork<User>
{
    Task<User> SearchByEmail(string email);
    Task DeleteByEmail(string email);
    Task<Token> Register(User user);
    Task<Token> Loggin(UserRequest userRequest);
    Task Logout(string refreshToken);
    Task<User> Update(User user);
    Task<Token> UpdatePassword(PasswordRequest passwordRequest, Guid id);
    Task<Token> ReadUser(User user);
    Task<IEnumerable<User>> ReadUsers();
    Task<Token> GetUserByToken(string token);
}
