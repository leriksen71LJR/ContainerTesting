using System;

namespace ContainerTests2.Contracts.Contexts
{
     public interface IContainerContext : IServiceContext, IServiceResolver, IDisposable
     {
     }
}