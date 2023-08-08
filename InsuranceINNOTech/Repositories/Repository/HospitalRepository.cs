namespace InsuranceINNOTech;

public class HospitalRepository : BaseSettingRepository<Hospital> , IHospitalRepository
{
	public HospitalRepository(ApplicationDbContext context) : base(context)
	{

	}
}
