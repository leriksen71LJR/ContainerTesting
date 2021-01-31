using Autofac;

namespace ContainerTests.Contracts
{
     public interface IContainerProfile : IServiceProfile
     {
          //all mocks will be automatically registered as their object type at the end of registration

          void RegisterModuleTypes(IModuleBuilder builder);

          void RegisterServiceTypes(ContainerBuilder builder);
     }
}