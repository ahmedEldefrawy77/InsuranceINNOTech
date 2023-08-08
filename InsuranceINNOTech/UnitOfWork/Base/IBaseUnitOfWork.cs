namespace InsuranceINNOTech;

public interface IBaseUnitOfWork<TEntity> where TEntity : BaseEntity
{
    Task Create(TEntity entity);

    Task UpdateAsync(TEntity entity);


    Task Delete(TEntity entity);
    Task Delete(Guid id);

    Task<IEnumerable<TEntity>> ReadAll();
    Task<TEntity> Read(Guid id);


}
