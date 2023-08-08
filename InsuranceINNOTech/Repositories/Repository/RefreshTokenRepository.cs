namespace InsuranceINNOTech;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(ApplicationDbContext context) : base(context){ }
        public override async Task<RefreshToken> Get(Guid id) => await Task.Run(()=> _entitySet.FirstOrDefault(e=> e.UserId== id)!);
        public override async Task Remove(Guid id) => await _entitySet.FirstOrDefaultAsync(e => e.Id == id);
}
