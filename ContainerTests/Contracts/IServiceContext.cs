using System;

namespace ContainerTests.Contracts
{
     public interface IServiceContext : IServiceResolver, IDisposable
     {
          void Build();

          void Refresh();
     }
}