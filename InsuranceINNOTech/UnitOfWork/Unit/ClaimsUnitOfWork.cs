namespace InsuranceINNOTech;

public class ClaimsUnitOfWork : BaseUnitOfWork<Claims> , IClaimsUnitOfWork
{
    private readonly IClaimsRepository _claimsRepository;
    private readonly ILogger<ClaimsUnitOfWork> _logger;
    private readonly IUserRepository _userRepository;
    public ClaimsUnitOfWork(IClaimsRepository claimsRepository,
                             ILogger<ClaimsUnitOfWork> logger,
                             IUserRepository userRepository) 
        : base(claimsRepository, logger)
    {
        _claimsRepository = claimsRepository;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Claims>> GetAllClaimsOfTheUser(User user)
    {
        User userFromDb = await _userRepository.Get(user.Id);
        if (userFromDb == null)
            throw new ArgumentNullException("there is no User in Data Base with such Id");
        
        IEnumerable<Claims> claims= new List<Claims>();
        using IDbContextTransaction transaction =await  _claimsRepository.GetTransaction();
        try
        {
           claims =  await _claimsRepository.Get(userFromDb.Id);
        }
        catch(Exception ex)
        {
            transaction.Rollback();
            _logger.LogError(ex.Message);
        }
        return claims;
    }
    public async Task<IEnumerable<Claims>> GetClaimsCreatedToday(User user)
    {
        User userFromDb = await _userRepository.Get(user.Id);
        if (userFromDb == null)
            throw new ArgumentNullException("there is no user in Db with such Id");

          return  (IEnumerable<Claims>)await _claimsRepository.GetClaimsCreatedToday(userFromDb.Id);
    }
}
