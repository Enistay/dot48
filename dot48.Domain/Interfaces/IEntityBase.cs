namespace dot48.Domain.Interfaces
{
    public interface IEntityBase<TKey> where TKey : struct
    {
        TKey Id { get; set; }
    }
}
