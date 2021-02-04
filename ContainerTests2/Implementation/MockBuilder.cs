using ContainerTests2.Contracts;
using ContainerTests2.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ContainerTests2.Implementation
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

          public void Register<T>(bool canRefresh = true,
               MockBehavior behavior = MockBehavior.Loose,
               Action<Mock<T>> configureAction = null,
               Func<T> factory = null)
               where T : class
          {
               var registration = new MockRegistration<T>(canRefresh, behavior, configureAction, factory);

               Register(registration);
          }

          private readonly List<IMockRegistration> _registrations;
     }
}