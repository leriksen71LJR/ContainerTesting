using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ContainerTests2.Contracts;
using ContainerTests2.Contracts.Profiles;

namespace ContainerTests2.Implementation.Profiles
{
     public class ContainerProfile : IContainerProfile
     {
          public ContainerProfile(Action<IModuleBuilder> registerModuleTypes, Action<ContainerBuilder> registerServiceTypes)
          {
               if (registerModuleTypes == null) throw new ArgumentNullException(nameof(registerModuleTypes));
               if (registerServiceTypes == null) throw new ArgumentNullException(nameof(registerServiceTypes));
               _registerModuleTypes = registerModuleTypes;
               _registerServiceTypes = registerServiceTypes;
          }

          private readonly Action<IModuleBuilder> _registerModuleTypes;

          private readonly Action<ContainerBuilder> _registerServiceTypes;


          #region explicit interface

          void IContainerProfile.RegisterModuleTypes(IModuleBuilder builder)
          {
               _registerModuleTypes?.Invoke(builder);
          }

          void IContainerProfile.RegisterServiceTypes(ContainerBuilder builder)
          {
               _registerServiceTypes?.Invoke(builder);
          }

          #endregion
     }
}
