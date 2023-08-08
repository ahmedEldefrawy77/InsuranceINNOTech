namespace InsuranceINNOTech;

public class PlaneRepository : BaseRepository<Plans> , IPlaneRepository
{
	public PlaneRepository(ApplicationDbContext context) : base(context)
	{
	}
}
