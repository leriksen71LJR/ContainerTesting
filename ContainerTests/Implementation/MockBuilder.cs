using System.Collections.Generic;
using System.Collections.Immutable;
using ContainerTests.Contracts;

namespace ContainerTests.Implementation
{
     public class MockBuilder : IMockBuilder
     {
          public MockBuilder()
          {
               _registrations = new List<IMockRegistration>();
          }

          public IImmutableList<IMockRegistration> GetRegistrations()
          {
               return _registrations.ToImmutableList();
          }

          public void Register(IMockRegistration mockRegistration)
          {
               if (mockRegistration == null)
               {
                    return;
               }

               _registrations.Add(mockRegistration);
          }

          private readonly List<IMockRegistration> _registrations;
     }
}