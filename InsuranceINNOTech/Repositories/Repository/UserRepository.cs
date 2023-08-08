        namespace InsuranceINNOTech;

public class UserRepository : BaseSettingRepository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context) { }

    public async override Task<User> Get(Guid id) =>
        await _entitySet.Include(e=> e.RefreshToken).FirstOrDefaultAsync(e => e.Id == id);
    public async Task<User> SearchByMail(string mail) =>
        await _entitySet.Include(e=> e.RefreshToken).FirstOrDefaultAsync(e => e.Mail! == mail);
    public async Task<IEnumerable<User>> Get() =>
        await _entitySet.Include(e => e.RefreshToken).ToListAsync();
    public async Task DeleteByMail(string mail)
    {
        User? userFromDb = await SearchByMail(mail);
        if (userFromDb == null)
            throw new ArgumentNullException("this User doesnot Exist in Db");

        await Task.Run(() => _entitySet.Remove(userFromDb));
    }

    public async Task<User> GetbyToken(string token)
    {
        if (token == null)
            throw new ArgumentNullException("token is invalid");

        return await _entitySet.Include(e=> e.RefreshToken).FirstOrDefaultAsync(e => e.RefreshToken.Value == token);
       
    }
   
    public async Task<bool> IsUserExist(string mail)
    {
        User user = await SearchByMail(mail);
      
        if(user != null)
        {
            return true;
        }
        return false;
    }

}
