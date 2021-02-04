using System;

namespace ContainerTests2.Contracts
{
     public interface IServiceResolver
     {
          object GetService(Type serviceType);
     }
}