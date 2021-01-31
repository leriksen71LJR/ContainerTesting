using System;

namespace ContainerTests.Contracts
{
     public interface ITestingContext : IMockResolver, IServiceResolver, IDisposable
     {
          void Refresh();
     }
}