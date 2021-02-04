using Autofac;

namespace ContainerTests2.Contracts.Profiles
{
     public interface IContainerProfile : IServiceProfile
     {
          void RegisterModuleTypes(IModuleBuilder builder);

          void RegisterServiceTypes(ContainerBuilder builder);
     }
}