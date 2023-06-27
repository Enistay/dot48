namespace dot48.Domain.Interfaces.Repository.Base
{ 
    public interface IWriteBehaviorRepository<TEntity, TKey>
    where TEntity : IEntityBase<TKey>
    where TKey : struct
    {
        TEntity SaveOrUpdate(TEntity entity);
        TEntity Include(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
