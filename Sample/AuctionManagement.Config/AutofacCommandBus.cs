using Autofac;
using Framework.Application;
using System.Threading.Tasks;

namespace AuctionManagement.Config
{
    public class AutofacCommandBus : ICommandBus
    {
        private readonly ILifetimeScope _scope;
        public AutofacCommandBus(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public async Task Dispatch<T>(T command)
        {
            var handler = _scope.Resolve<ICommandHandler<T>>();
            await handler.Handle(command);
        }
    }
}