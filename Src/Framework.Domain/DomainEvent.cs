using System;

namespace Framework.Domain
{
    public abstract class DomainEvent
    {
        public Guid EventId { get; private set; }
        public DateTime PublishDateTime { get; private set; }
        protected DomainEvent()
        {
            this.PublishDateTime = DateTime.Now;
            this.EventId  = Guid.NewGuid();    
        }
    }
}
