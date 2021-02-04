using ContainerTests2.Contracts;
using ContainerTests2.Contracts.Contexts;
using ContainerTests2.Contracts.Profiles;
using Moq;
using System;
using System.Collections.Immutable;
using System.Linq;

namespace ContainerTests2.Implementation.Contexts
{
     public class LightweightContext : ILightweightContext
     {
          public LightweightContext(IContextProvider contextProvider, ILightweightProfile profile)
          {
               _contextProvider = contextProvider;
               _profile = profile;
          }

          public static IServiceContext Create(IContextProvider provider, IServiceProfile profile)
          {
               if (provider.GetContext<ILightweightContext>() != null)
               {
                    throw new InvalidOperationException($"Only one instance of '{nameof(ILightweightProfile)}' is allowed.");
               }

               // ReSharper disable once UseNegatedPatternMatching
               // ReSharper disable once SuspiciousTypeConversion.Global
               var lightweightProfile = (ILightweightProfile)profile;

               return lightweightProfile == null ? null : new LightweightContext(provider, lightweightProfile);
          }

          public void Build()
          {
               lock (SYNC_LOCK)
               {
                    var serviceBuilder = new ServiceBuilder();

                    _profile.RegisterServices(serviceBuilder, GetMockResolver());

                    _services = serviceBuilder.GetServices();
               }
          }

          public object GetService(Type serviceType)
          {
               if (serviceType == null)
               {
                    return null;
               }

               return _services.FirstOrDefault(serviceType.IsInstanceOfType);
          }

          public void Refresh()
          {
               Build();
          }

          private IMockResolver GetMockResolver()
          {
               var mockContext = _contextProvider.GetContext<IMockContext>();

               return mockContext != null
                    ? (IMockResolver)mockContext
                    : new NotImplementedMockResolver();
          }

          private static object SYNC_LOCK = new object();

          private readonly IContextProvider _contextProvider;

          private readonly ILightweightProfile _profile;

          private IImmutableList<object> _services;

          private class NotImplementedMockResolver : IMockResolver
          {
               public Mock<T> GetMock<T>() where T : class
               {
                    throw new InvalidOperationException($"Instance of {nameof(IMockProfile)} not registered.");
               }
          }
     }
}