using System;

namespace ContainerTests.Contracts
{
     public interface IModuleBuilder
     {
          void AddRange(params Type[] moduleTypes);
     }
}