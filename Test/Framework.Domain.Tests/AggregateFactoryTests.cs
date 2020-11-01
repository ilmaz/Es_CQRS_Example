using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using FizzWare.NBuilder;
using FluentAssertions;
using Xunit;

namespace Framework.Domain.Tests
{
    public class AggregateFactoryTests
    {
        [Fact]
        public void creates_aggregate_by_applying_events()
        {
            var events = new List<DomainEvent>()
            {
                new UserRegistered(1, "admin", "john", "doe"),
                new UserActivated(1),
                new UserPersonalInfoUpdated("jack", "doe")
            };
            var factory = new AggregateFactory();

            var user = factory.Create<User>(events);

            user.IsActive.Should().BeTrue();
            user.Firstname.Should().Be("jack");
            user.Lastname.Should().Be("doe");
            user.Username.Should().Be("admin");
        }
    }   

    public class User : AggregateRoot<long>
    {
        public string Username { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get;private  set; }
        public bool IsActive { get;  private set; }
        private User() { }
        public void Activate()
        {
            Causes(new UserActivated(this.Id));
        }
        public void ChangePersonalInfo(string firstname, string lastname)
        {
            if (!this.IsActive)
                throw new Exception(".......");

            Causes(new UserPersonalInfoUpdated(firstname, lastname));
        }
        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }
        private void When(UserRegistered @event)
        {
            this.Id = @event.Id;
            this.Firstname = @event.Firstname;
            this.Lastname = @event.Lastname;
            this.Username = @event.Username;
            this.IsActive = false;
        }
        private void When(UserActivated @event)
        {
            this.IsActive = true;
        }
        private void When(UserPersonalInfoUpdated @event)
        {
            this.Firstname = @event.Firstname;
            this.Lastname = @event.Lastname;
        }
    }
    public class UserRegistered : DomainEvent
    {
        public long Id { get;private set; }
        public string Username { get; private set; }
        public string Firstname { get; private set; }
        public string Lastname { get; private set; }
        public UserRegistered(long id, string username, string firstname, string lastname)
        {
            Id = id;
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
        }
    }
    public class UserPersonalInfoUpdated : DomainEvent
    {
        public string Firstname { get;  set; }
        public string Lastname { get; set; }

        public UserPersonalInfoUpdated()
        {
            
        }
        public UserPersonalInfoUpdated(string firstname, string lastname)
        {
            Firstname = firstname;
            Lastname = lastname;
        }
    }
    public class UserActivated : DomainEvent
    {
        public long UserId { get; private set; }
        public UserActivated(long userId)
        {
            UserId = userId;
        }
    }
}