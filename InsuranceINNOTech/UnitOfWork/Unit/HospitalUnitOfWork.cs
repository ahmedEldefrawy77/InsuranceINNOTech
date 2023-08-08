namespace InsuranceINNOTech;

public class HospitalUnitOfWork : BaseSettingUnitOfWork<Hospital> , IHospitalUnitOfWork
{
    private readonly IHospitalRepository _hospitalRepository;
    private readonly ILogger<HospitalUnitOfWork> _logger;
    public HospitalUnitOfWork(IHospitalRepository hospitalRepository, ILogger<HospitalUnitOfWork> logger) : base(hospitalRepository, logger)
    {
        _hospitalRepository= hospitalRepository;
        _logger= logger;
    }

}
