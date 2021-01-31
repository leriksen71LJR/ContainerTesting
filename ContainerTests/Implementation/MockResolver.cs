using System;
using System.Collections.Immutable;
using System.Linq;
using ContainerTests.Contracts;
using Moq;

namespace ContainerTests.Implementation
{
     public class MockResolver : IMockResolver
     {
          public MockResolver(IImmutableList<IMockRegistration> registrations)
          {
               _registrations = registrations ?? throw new ArgumentNullException(nameof(registrations));
          }

          public Mock<T> GetMock<T>() where T : class
          {
               return _registrations.Select(r => r.Mock).OfType<Mock<T>>().FirstOrDefault();
          }

          private readonly IImmutableList<IMockRegistration> _registrations;
     }
}