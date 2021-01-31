using System;
using System.Collections.Immutable;
using System.Linq;
using ContainerTests.Contracts;

namespace ContainerTests.Implementation
{
     public class LightweightContext : IServiceContext
     {
          public LightweightContext(ILightweightProfile profile, IMockResolver resolver)
          {
               _profile = profile ?? throw new ArgumentNullException(nameof(profile));
               _mockResolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
          }

          public void Build()
          {
               lock (SYNC_LOCK)
               {
                    var serviceBuilder = new ServiceBuilder();

                    _profile.RegisterServices(serviceBuilder, _mockResolver);

                    _services = serviceBuilder.GetServices();
               }
          }

          public void Refresh()
          {
               Build();
          }

          public TService GetService<TService>()
          {
               var result = _services.OfType<TService>().FirstOrDefault();

               if (result != null)
               {
                    return result;
               }

               throw new InvalidOperationException($"Cannot find service of type '{typeof(TService).Name}'.");
          }

          public void Dispose()
          {
               //do nothing
          }

          private static object SYNC_LOCK = new object();

          private IImmutableList<object> _services;

          private readonly ILightweightProfile _profile;
          private readonly IMockResolver _mockResolver;
     }
}