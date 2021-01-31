using System;
using Autofac;
using ContainerTests.Contracts;

namespace ContainerTests.Models
{
     public class ContainerProfile : IContainerProfile
     {
          public ContainerProfile(Action<IModuleBuilder> registerModuleTypes, Action<ContainerBuilder> registerServiceTypes)
          {
               _registerModuleTypes = registerModuleTypes ?? throw new ArgumentNullException(nameof(registerModuleTypes));
               _registerServiceTypes = registerServiceTypes ?? throw new ArgumentNullException(nameof(registerServiceTypes));
          }

          private readonly Action<IModuleBuilder> _registerModuleTypes;

          private readonly Action<ContainerBuilder> _registerServiceTypes;

          #region interface explicit

          void IContainerProfile.RegisterModuleTypes(IModuleBuilder builder)
          {
               _registerModuleTypes.Invoke(builder);
          }

          void IContainerProfile.RegisterServiceTypes(ContainerBuilder builder)
          {
               _registerServiceTypes.Invoke(builder);
          }

          #endregion interface explicit
     }
}