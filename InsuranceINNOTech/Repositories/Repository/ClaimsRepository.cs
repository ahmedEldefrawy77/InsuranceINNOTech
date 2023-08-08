namespace InsuranceINNOTech;
    public class ClaimsRepository : BaseRepository<Claims> , IClaimsRepository
    {
      public ClaimsRepository(ApplicationDbContext context) : base(context) { }


    public new async Task<IEnumerable<Claims>> Get(Guid id) => await _entitySet.Where(e => e.userId== id).ToListAsync();

    public async Task<IEnumerable<Claim>> GetClaimsCreatedToday(Guid id)
    {
       IEnumerable<Claims> claims = await _entitySet.Where(e => e.userId == id)
                                 .Where(e => e.DateCreatedAt == DateTime.UtcNow.Date).ToListAsync();
        return (IEnumerable<Claim>)claims;
    }
}

