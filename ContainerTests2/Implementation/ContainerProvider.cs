using ContainerTests2.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerTests2.Implementation
{
     public class ContainerProvider : IContextProvider
     {
          public ContainerProvider(IList<IServiceContext> contexts)
          {
               _contexts = contexts;
          }

          public T GetContext<T>() where T : class, IServiceContext
          {
               return _contexts.OfType<T>().FirstOrDefault();
          }

          public IList<T> GetContexts<T>() where T : class, IServiceContext
          {
               return _contexts.OfType<T>().ToList();
          }

          private readonly IList<IServiceContext> _contexts;
     }
}