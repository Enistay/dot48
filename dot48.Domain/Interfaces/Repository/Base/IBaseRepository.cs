namespace dot48.Domain.Interfaces.Repository.Base
{
    public interface IBaseRepository<TEntity, TKey> : IReadBehaviorRepository<TEntity, TKey>, IWriteBehaviorRepository<TEntity, TKey>
       where TEntity : IEntityBase<TKey>
       where TKey : struct
    {
    }
}
