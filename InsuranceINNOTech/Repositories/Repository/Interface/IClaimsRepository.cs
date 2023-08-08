namespace InsuranceINNOTech;
    public interface IClaimsRepository : IBaseRepository<Claims>
    {
     Task<IEnumerable<Claims>> Get(Guid id);
     Task<IEnumerable<Claim>> GetClaimsCreatedToday(Guid id);
    }

