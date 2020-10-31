using Autofac;
using Framework.Application;

namespace AuctionManagement.Config
{
    public class AutofacCommandBus : ICommandBus
    {
        private readonly ILifetimeScope _scope;
        public AutofacCommandBus(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Dispatch<T>(T command)
        {
            var handler = _scope.Resolve<ICommandHandler<T>>();
            handler.Handle(command);
        }
    }
}