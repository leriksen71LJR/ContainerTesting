using ContainerTests2.Contracts;
using ContainerTests2.Implementation.Contexts;
using System;
using System.Collections.Generic;

namespace ContainerTests2.Implementation
{
     public class ContextFactory : IContextFactory
     {
          public ContextFactory(IContextProvider contextProvider)
          {
               _contextProvider = contextProvider;
          }

          public IServiceContext GetContext(IServiceProfile profile)
          {
               if (profile == null) throw new ArgumentNullException(nameof(profile));

               foreach (var builder in _builders)
               {
                    var result = builder(_contextProvider, profile);

                    if (result != null)
                    {
                         return result;
                    }
               }

               throw new InvalidOperationException(
                    $"Cannot build context for profile of type '{profile.GetType().Name}'.");
          }

          private readonly IList<Func<IContextProvider, IServiceProfile, IServiceContext>> _builders =
               new List<Func<IContextProvider, IServiceProfile, IServiceContext>>
               {
                    MockContext.Create,
                    ContainerContext.Create,
                    LightweightContext.Create
               };

          private readonly IContextProvider _contextProvider;
     }
}