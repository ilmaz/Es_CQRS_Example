namespace Framework.Domain
{
    public interface IAggregateRoot
    {
        void Apply(DomainEvent @event);
    }
}