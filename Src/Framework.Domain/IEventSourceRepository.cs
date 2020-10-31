using System.Threading.Tasks;

namespace Framework.Domain
{
    public interface IEventSourceRepository<T, TKey> where T : AggregateRoot<TKey>
    {
        Task AppendEvents(T aggregate);
        Task<T> GetById(TKey id);
    }
}