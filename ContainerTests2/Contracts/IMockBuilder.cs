using Moq;
using System;

namespace ContainerTests2.Contracts
{
     public interface IMockBuilder
     {
          void Register(IMockRegistration mockRegistration);

          void Register<T>(bool canRefresh = true,
               MockBehavior behavior = MockBehavior.Loose,
               Action<Mock<T>> configureAction = null,
               Func<T> factory = null)
               where T : class;
     }
}