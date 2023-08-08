namespace InsuranceINNOTech;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> Get(Guid id);
    Task<IEnumerable<TEntity>> GetAll();

    Task Add(TEntity entity);
   
    Task Update(TEntity entity);

    Task Remove(Guid id);
    Task Remove(TEntity entity);

    Task<IDbContextTransaction> GetTransaction();

}
