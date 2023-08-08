namespace InsuranceINNOTech;

public class DependentUnitOfWork : BaseSettingUnitOfWork<Dependent> , IDependentUnitOfWork
{
    private readonly IDependentRepository _dependentRepository;
	private readonly ILogger<DependentUnitOfWork> _logger;
    private readonly IUserRepository _userRepository;
	public DependentUnitOfWork(IDependentRepository dependentRepositroy, ILogger<DependentUnitOfWork> logger, IUserRepository userRepository) 
        :base (dependentRepositroy , logger)

	{
        _logger= logger;
		_dependentRepository= dependentRepositroy;
        _userRepository= userRepository;
	}

    public async Task DeleteDependent(User user , string Email)
    {
        using IDbContextTransaction transaction = await _dependentRepository.GetTransaction();
        try
        {
            await _dependentRepository.DeleteDependent(user.Id, Email);
        }
        catch(Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex.Message);
        }
        await transaction.CommitAsync();
    }

    public async Task<IEnumerable<Dependent>> GetDependentOfUser(User user)
    {
      User userFormDb = await _userRepository.Get(user.Id);
        if (userFormDb == null)
            throw new ArgumentNullException("there is no user with this Id");

       return await _dependentRepository.GetDependentAsync(user.Id);
    }
}
