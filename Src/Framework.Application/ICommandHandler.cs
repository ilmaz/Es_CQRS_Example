using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Application
{
    public interface ICommandHandler<T>
    {
        void Handle(T command);
    }
}
