namespace Framework.Domain
{
    public interface IEventSourceRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        void AppendEvents(T aggregate);
        T GetById(TKey id);
    }
}