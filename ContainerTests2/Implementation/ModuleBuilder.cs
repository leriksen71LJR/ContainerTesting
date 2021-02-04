using ContainerTests2.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace ContainerTests2.Implementation
{
     public class ModuleBuilder : IModuleBuilder
     {
          public ModuleBuilder()
          {
               _moduleTypes = new List<Type>();
          }

          public void AddRange(params Type[] moduleTypes)
          {
               if (moduleTypes == null)
               {
                    return;
               }

               _moduleTypes.AddRange(moduleTypes.Where(m => m != null).ToList());
          }

          public IImmutableList<Type> GetModuleTypes()
          {
               return _moduleTypes.ToImmutableList();
          }

          private readonly List<Type> _moduleTypes;
     }
}