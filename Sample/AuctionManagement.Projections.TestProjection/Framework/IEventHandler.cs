using Framework.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AuctionManagement.Projections.TestProjection.Framework
{

    public interface IEventHandler<T> where T : DomainEvent
    {
        void Handle(T domainEvent);
    }
}
