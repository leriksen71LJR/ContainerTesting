using System;
using System.Linq;
using Autofac;
using ContainerTests.Contracts;

namespace ContainerTests.Implementation
{
     public class ContainerContext : IServiceContext
     {
          public ContainerContext(IContainerProfile profile, IMockProvider provider)
          {
               _profile = profile ?? throw new ArgumentNullException(nameof(profile));
               _provider = provider ?? throw new ArgumentNullException(nameof(provider));
          }

          public void Build()
          {
               lock (SYNC_LOCK)
               {
                    _container?.Dispose();
                    var builder = new ContainerBuilder();

                    RegisterModuleTypes(builder);
                    RegisterServiceTypes(builder);
                    RegisterServiceMocks(builder);

                    _container = builder.Build();
               }
          }

          public void Refresh()
          {
               Build();
          }

          public TService GetService<TService>()
          {
               return _container.Resolve<TService>();
          }

          private void RegisterServiceTypes(ContainerBuilder builder)
          {
               _profile.RegisterServiceTypes(builder);
          }

          private void RegisterModuleTypes(ContainerBuilder builder)
          {
               var moduleBuilder = new ModuleBuilder();

               _profile.RegisterModuleTypes(moduleBuilder);

               var moduleTypes = moduleBuilder.GetModuleTypes();

               if (!moduleTypes.Any()) return;

               foreach (var type in moduleTypes)
               {
                    builder.RegisterAssemblyModules(type, type.Assembly);
               }
          }

          private void RegisterServiceMocks(ContainerBuilder builder)
          {
               var serviceMocks = _provider.GetRegistrations();

               if (!serviceMocks.Any()) return;

               foreach (var registration in serviceMocks)
               {
                    var serviceType = registration.Mock.GetType().GetGenericArguments().First();

                    builder.RegisterInstance(registration.Mock.Object).As(serviceType);
               }
          }

          public void Dispose()
          {
              _container?.Dispose();
          }

          private static object SYNC_LOCK = new object();

          private IContainer _container;

          private readonly IContainerProfile _profile;
          private readonly IMockProvider _provider;
     }
}