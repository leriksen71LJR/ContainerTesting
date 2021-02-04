using ContainerTests2.Contracts;
using ContainerTests2.Contracts.Contexts;
using ContainerTests2.Contracts.Profiles;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ContainerTests2.Implementation.Contexts
{
     public class MockContext : IMockContext
     {
          public MockContext(IMockProfile profile)
          {
               if (profile == null) throw new ArgumentNullException(nameof(profile));
               _profile = profile;
          }

          public static IServiceContext Create(IContextProvider provider, IServiceProfile profile)
          {
               if (provider.GetContext<IMockContext>() != null)
               {
                    throw new InvalidOperationException($"Only one instance of '{nameof(IMockProfile)}' is allowed.");
               }

               // ReSharper disable once UseNegatedPatternMatching
               // ReSharper disable once SuspiciousTypeConversion.Global
               var mockProfile = (IMockProfile)profile;

               return mockProfile == null ? null : new MockContext(mockProfile);
          }

          public void Build()
          {
               var mockBuilder = new MockBuilder();

               _profile.RegisterMocks(mockBuilder);

               _registrations = mockBuilder.GetRegistrations();

               Refresh();
          }

          public Mock<T> GetMock<T>() where T : class
          {
               return GetService(typeof(Mock<T>)) as Mock<T>;
          }

          public IList<Mock> GetMocks()
          {
               return _registrations.Select(r => r.Mock).ToList();
          }

          public object GetService(Type serviceType)
          {
               if (serviceType == null)
               {
                    return null;
               }

               return _registrations
                    .Select(r => r.Mock)
                    .FirstOrDefault(serviceType.IsInstanceOfType);
          }

          public void Refresh()
          {
               Build();
          }

          private readonly IMockProfile _profile;

          private IImmutableList<IMockRegistration> _registrations;
     }
}