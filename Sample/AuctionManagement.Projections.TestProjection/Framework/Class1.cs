using System;
using System.Collections.Generic;
using System.Text;
using Framework.Domain;

namespace AuctionManagement.Projections.TestProjection.EventHandlers
{
    public interface IEventHandler<T> where T : DomainEvent
    {
        void Handle(T domainEvent);
    }
}
