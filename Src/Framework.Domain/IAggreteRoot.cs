namespace Framework.Domain
{
    public interface IAggreteRoot
    {
        void Apply(DomainEvent @event);
    }
}
