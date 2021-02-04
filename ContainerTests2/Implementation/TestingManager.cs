using ContainerTests2.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerTests2.Implementation
{
     public class TestingManager : IServiceResolver, IDisposable
     {
          public TestingManager(IList<IServiceProfile> profiles)
          {
               _profiles = profiles ?? new List<IServiceProfile>();

               _contexts = new List<IServiceContext>();

               _provider = new ContainerProvider(_contexts);

               CreateContexts();
               BuildContexts();
          }

          public void Dispose()
          {
               var disposers = _contexts.OfType<IDisposable>().ToList();

               foreach (var disposable in disposers)
               {
                    disposable.Dispose();
               }
          }

          public object GetService(Type serviceType)
          {
               if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));

               var resolvers = _contexts.OfType<IServiceResolver>().ToList();

               foreach (var resolver in resolvers)
               {
                    try
                    {
                         var result = resolver.GetService(serviceType);

                         if (result != null)
                         {
                              return result;
                         }
                    }
                    catch (Exception ex)
                    {
                         throw new Exception(
                              $"Error finding service of type '{serviceType.Namespace}' in context of type '{resolver.GetType().Name}'.",
                              ex);
                    }
               }

               throw new InvalidOperationException($"Cannot find instance for service type '{serviceType.Name}'.");
          }

          public void Refresh()
          {
               foreach (var context in _contexts)
               {
                    context.Refresh();
               }
          }

          private void BuildContexts()
          {
               foreach (var context in _contexts)
               {
                    context.Build();
               }
          }

          private void CreateContexts()
          {
               var factory = new ContextFactory(_provider);

               foreach (var profile in _profiles)
               {
                    var context = factory.GetContext(profile);

                    _contexts.Add(context);
               }
          }

          private readonly IList<IServiceContext> _contexts;

          private readonly IList<IServiceProfile> _profiles;

          private readonly IContextProvider _provider;
     }
}