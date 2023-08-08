namespace InsuranceINNOTech;

public class DependentRepository : BaseSettingRepository<Dependent> , IDependentRepository
{
	public DependentRepository(ApplicationDbContext context) : base(context)
	{

	}

    public async Task<IEnumerable<Dependent>> GetDependentAsync(Guid id) => await _entitySet.Where(e=> e.UserId== id).ToListAsync();
	public async Task<Dependent> GetDependentByEmail(string mail) => await Task.Run(()=>_entitySet.FirstOrDefaultAsync(e => e.Email == mail)!);
	public async Task DeleteDependent(Guid id, string Email)
	{
		Dependent dependentFromDb = await this.GetDependentByEmail(Email);
		if (dependentFromDb == null)
			throw new ArgumentNullException("there is no Dependent with this email");

		if (dependentFromDb.UserId == id)
			await Task.Run(()=> _entitySet.Remove(dependentFromDb));
	}
}
