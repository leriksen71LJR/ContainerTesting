using Moq;
using System.Collections.Generic;

namespace ContainerTests2.Contracts.Contexts
{
     public interface IMockContext : IServiceContext, IServiceResolver, IMockResolver
     {
          IList<Mock> GetMocks();
     }
}