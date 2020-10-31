using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Domain
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }
    }
}
