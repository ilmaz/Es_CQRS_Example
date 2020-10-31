using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface ICommandHandler<T>
    {
        Task Handle(T command);
    }
}
