using System.Collections.Generic;

namespace ContainerTests2.Contracts
{
     public interface IContextProvider
     {
          T GetContext<T>() where T : class, IServiceContext;

          IList<T> GetContexts<T>() where T : class, IServiceContext;
     }
}