namespace InsuranceINNOTech;

public class PlaneUnitOfWork : BaseUnitOfWork<Plans> , IPlaneUnitOfWork
{
    private readonly IPlaneRepository _planRepository;
    private readonly ILogger<PlaneUnitOfWork> _logger;
    public PlaneUnitOfWork(IPlaneRepository planeRepository , ILogger<PlaneUnitOfWork> logger) : base (planeRepository, logger)
    {
        _planRepository = planeRepository;
        _logger = logger;
    }

}
