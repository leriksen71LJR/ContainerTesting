using Autofac;
using ContainerTests2.Contracts;
using ContainerTests2.Contracts.Contexts;
using ContainerTests2.Contracts.Profiles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerTests2.Implementation.Contexts
{
     public class ContainerContext : IContainerContext
     {
          public ContainerContext(IContextProvider contextProvider, IContainerProfile profile)
          {
               _profile = profile;
               _contextProvider = contextProvider;
          }

          public static IServiceContext Create(IContextProvider provider, IServiceProfile profile)
          {
               if (provider.GetContext<IContainerContext>() != null)
               {
                    throw new InvalidOperationException($"Only one instance of '{nameof(IContainerProfile)}' is allowed.");
               }

               // ReSharper disable once UseNegatedPatternMatching
               // ReSharper disable once SuspiciousTypeConversion.Global
               var containerProfile = (IContainerProfile)profile;

               return containerProfile == null ? null : new ContainerContext(provider, containerProfile);
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

          public void Dispose()
          {
               _container?.Dispose();
          }

          public object GetService(Type serviceType)
          {
               if (serviceType == null || !_container.IsRegistered(serviceType))
               {
                    return null;
               }
               
               return _container.Resolve(serviceType);
          }

          public void Refresh()
          {
               Build();
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
               var serviceMocks = _contextProvider.GetContext<IMockContext>()?.GetMocks() ?? new List<Mock>();

               if (!serviceMocks.Any()) return;

               foreach (var serviceMock in serviceMocks)
               {
                    builder.RegisterInstance(serviceMock.Object)
                         .As(serviceMock.GetType().GetGenericArguments().First());
               }
          }

          private void RegisterServiceTypes(ContainerBuilder builder)
          {
               _profile.RegisterServiceTypes(builder);
          }

          private static object SYNC_LOCK = new object();

          private readonly IContextProvider _contextProvider;

          private readonly IContainerProfile _profile;

          private IContainer _container;
     }
}