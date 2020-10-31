using FluentAssertions;
using System;
using Xunit;

namespace Framework.Domain.Tests
{
    public class DomainEventTests
    {
        private class UserRegistered : DomainEvent { }
        [Fact]
        public void each_event_has_a_unique_identifier()
        {
            var @event = new UserRegistered();
            @event.EventId.Should().NotBe(Guid.Empty);
        }
    }
}
