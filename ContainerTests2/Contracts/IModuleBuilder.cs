using System;

namespace ContainerTests2.Contracts
{
     public interface IModuleBuilder
     {
          void AddRange(params Type[] moduleTypes);
     }
}