namespace InsuranceINNOTech;

public class BaseUnitOfWork<TEntity> : IBaseUnitOfWork<TEntity> where TEntity : BaseEntity
{
    private readonly ILogger<BaseUnitOfWork<TEntity>> _logger;
    private readonly IBaseRepository<TEntity> _Repository;
    public BaseUnitOfWork(IBaseRepository<TEntity> repository, ILogger<BaseUnitOfWork<TEntity>> logger)
    {
        _Repository = repository;
        _logger = logger;
    }

    public async Task<TEntity> Read(Guid id) =>
        await _Repository.Get(id);

    public async virtual Task Create(TEntity entity)
    {
        using IDbContextTransaction transaction = await _Repository.GetTransaction();
        try
        {
            await _Repository.Add(entity);
        }
        catch (Exception ex)
        {

            transaction.Rollback();

            _logger.LogError(ex.Message);

        }
        await transaction.CommitAsync();

    }

    public async Task Delete(TEntity entity)
    {
        using IDbContextTransaction transaction = await _Repository.GetTransaction();

        try
        {
            await _Repository.Remove(entity);

        }
        catch (Exception ex)
        {
            transaction.Rollback();
           

            _logger.LogError(ex.Message);
        }

        await transaction.CommitAsync();
    }

    public async Task Delete(Guid id)
    {
        using IDbContextTransaction transaction = await _Repository.GetTransaction();

        try
        {
            await _Repository.Remove(id);

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            _logger.LogError(ex.Message);
        }

        await transaction.CommitAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        using IDbContextTransaction transaction = await _Repository.GetTransaction();
        try
        {
            await _Repository.Update(entity);
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            _logger.LogError(ex.Message);
        }
        await transaction.CommitAsync();
    }

    public async Task<IEnumerable<TEntity>> ReadAll()
    {
        return await _Repository.GetAll();

    }
}
