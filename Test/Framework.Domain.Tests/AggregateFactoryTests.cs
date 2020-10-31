using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Framework.Domain.Tests
{
    public class AggregateFactoryTests
    {
        //[Fact]
        //public void just_for_class()
        //{
        //    //==>Load & Replay Events
        //    var userRegistered = new UserRegistered() { UserName = "Admin", FirstName = "ilmaz", LastNAme = "sourani" };
        //    //var userActivated = new UserActivated() { UserId = 1 };

        //    var user = new User();
        //    user.Apply(userRegistered);


        //    //user.Apply(userActivated);

        //    //user.UserName.Should().Be("Admin");
        //    //user.FirstName.Should().Be("ilmaz");
        //    //user.LastNAme.Should().Be("sourani");
        //    //user.IsActive.Should().BeTrue();


        //    user.Activate();

        //    //Save Events into DataBase
        //    var events = user.GetUncommittedEvents();
        //}

        [Fact]
        public void creates_aggregate_by_applying_events()
        {
            var events = new List<DomainEvent>()
            {
                new UserRegistered(1,"admin","ilmaz","sourani"),
                new UserActivated(1),
                new UserPersonalInfoUpdated("araz","s")
            };

            var factory = new AggregateFactory();

            var user = factory.Create<User>(events);

            user.UserName.Should().Be("admin");
            user.FirstName.Should().Be("araz");
            user.LastNAme.Should().Be("s");
            user.IsActive.Should().BeTrue();
        }
    }

    public class User : AggregateRoot<long>
    {
        //public long Id { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastNAme { get; private set; }
        public bool IsActive { get; private set; }

        private User()
        {

        }

        public void Activate()
        {
            //Some Invariants...

            Causes(new UserActivated(Id));
        }

        public void ChangePersonalInfo(string firstname, string lastname)
        {
            if (!IsActive)
                throw new Exception();

            Causes(new UserPersonalInfoUpdated(firstname, lastname));
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }

        private void When(UserRegistered @event)
        {
            Id = @event.Id;
            UserName = @event.UserName;
            FirstName = @event.FirstName;
            LastNAme = @event.LastNAme;
            IsActive = false;
        }

        private void When(UserActivated @event)
        {
            IsActive = true;
        }

        private void When(UserPersonalInfoUpdated @event)
        {
            FirstName = @event.FirstName;
            LastNAme = @event.LastNAme;
        }
    }

    public class UserPersonalInfoUpdated : DomainEvent
    {
        public string FirstName { get; private set; }
        public string LastNAme { get; private set; }

        public UserPersonalInfoUpdated(string firstName, string lastNAme)
        {
            FirstName = firstName;
            LastNAme = lastNAme;
        }
    }

    public class UserRegistered : DomainEvent
    {
        public long Id { get; private set; }
        public string UserName { get; private set; }
        public string FirstName { get; private set; }
        public string LastNAme { get; private set; }

        public UserRegistered(long id, string userName, string firstName, string lastNAme)
        {
            Id = id;
            UserName = userName;
            FirstName = firstName;
            LastNAme = lastNAme;
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
