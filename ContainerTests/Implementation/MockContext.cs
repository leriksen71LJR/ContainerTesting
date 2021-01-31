using System.Collections.Immutable;
using System.Linq;
using ContainerTests.Contracts;
using Moq;

namespace ContainerTests.Implementation
{
     public class MockContext : IMockContext
     {
          public MockContext(IMockProfile profile)
          {
               _profile = profile;
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
               return _registrations.Select(r => r.Mock).OfType<Mock<T>>().FirstOrDefault();
          }

          public IImmutableList<IMockRegistration> GetRegistrations()
          {
               return _registrations.ToImmutableList();
          }

          public void Refresh()
          {
               foreach (var registration in _registrations)
               {
                    registration.Refresh();
               }
          }

          private readonly IMockProfile _profile;

          private IImmutableList<IMockRegistration> _registrations;
     }
}