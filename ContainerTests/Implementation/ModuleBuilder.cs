using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ContainerTests.Contracts;

namespace ContainerTests.Implementation
{
     public class ModuleBuilder : IModuleBuilder
     {
          public ModuleBuilder()
          {
               _moduleTypes = new List<Type>();
          }

          public IImmutableList<Type> GetModuleTypes()
          {
               return _moduleTypes.ToImmutableList();
          }

          public void AddRange(params Type[] moduleTypes)
          {
               if (moduleTypes == null)
               {
                    return;
               }

               _moduleTypes.AddRange(moduleTypes.Where(m => m != null).ToList());
          }

          private readonly List<Type> _moduleTypes;
     }
}