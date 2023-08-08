namespace InsuranceINNOTech;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected DbSet<TEntity> _entitySet;
    private readonly ApplicationDbContext _context;
    public BaseRepository(ApplicationDbContext context)
    {
        _context = context;
        _entitySet = _context.Set<TEntity>();
    }

    public virtual async Task Add(TEntity entity)
    {
        ThrowExceptionIfParamaternotSupplied(entity);
        await _entitySet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(TEntity entity)
    {
        if (entity == null || entity.Id == Guid.Empty)
        {
            throw new ArgumentNullException("there is no entity with such Id ");
        }
        TEntity? entityFromDb = await Get(entity.Id);
        if (entityFromDb == null)
            throw new ArgumentNullException($"nameof{entity}" + "this entity is not provided in DataBase");

        await Task.Run(() => _context.Remove(entityFromDb));
        await _context.SaveChangesAsync();
    }

    public async virtual Task Remove(Guid id)
    {
        TEntity? entityFromDb = await Get(id);
        if (entityFromDb == null)
            throw new ArgumentNullException("there is no Data With such id");

        await Task.Run(() => _entitySet.Remove(entityFromDb));
        await _context.SaveChangesAsync();
    }

    public async virtual Task<IEnumerable<TEntity>> GetAll() => await _entitySet.ToListAsync();
    public virtual async Task<TEntity> Get(Guid id)
    {
        TEntity? UserFromDb = await _entitySet.FirstOrDefaultAsync(e => e.Id == id);
        if (UserFromDb == null)
            throw new ArgumentException("there is no User in Db with this Id");

        return UserFromDb;
    }

    public async Task Update(TEntity entity)
    {
        ThrowExceptionIfParamaternotSupplied(entity);
        await ThrowExceptionIfEntityExistInDataBase(entity);
        await Task.Run(() => _context.Update(entity));
        await _context.SaveChangesAsync();
    }

    private static void ThrowExceptionIfParamaternotSupplied(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("there is no Paramater Supplied");

    }

    private static void ThrowExceptionIfParamaternotSupplied(IEnumerable<TEntity> entities)
    {
        if (entities == null || !entities.Any())
            throw new ArgumentNullException($"nameof{entities}" + "there is no parameter supplied");
    }
    private async Task ThrowExceptionIfEntityExistInDataBase(TEntity entity)
    {
        TEntity? entityFromDb = await Get(entity.Id);
        if (entityFromDb == null)
            throw new ArgumentException("this entity not found in Db");
    }

    
    public async Task<IDbContextTransaction> GetTransaction() => await _context.Database.BeginTransactionAsync();


}
