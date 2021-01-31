using System;
using System.Collections.Generic;
using ContainerTests.Contracts;
using Moq;

namespace ContainerTests.Implementation
{
     public class TestingContext : ITestingContext
     {
          public TestingContext(ITestingProfile profile)
          {
               if (profile == null) throw new ArgumentNullException(nameof(profile));

               _mockContext = new MockContext(profile.Mocks);

               _mockContext.Build();

               _serviceContext = GetContext(profile.Services);

               _serviceContext.Build();

               _refreshMocks = profile.RefreshMocks;

               _reuseServices = profile.ReuseServices;
          }

          public void Dispose()
          {
               if (_disposed) return;

               this._serviceContext.Dispose();

               _disposed = true;
          }

          public Mock<T> GetMock<T>() where T : class
          {
               return _mockContext.GetMock<T>();
          }

          public TService GetService<TService>()
          {
               return _serviceContext.GetService<TService>();
          }

          public void Refresh()
          {
               if (_refreshMocks)
               {
                    _mockContext.Refresh();
               }

               if (!_reuseServices)
               {
                    _serviceContext.Refresh();
               }
          }

          private IServiceContext GetContext(IServiceProfile profile)
          {
               foreach (var handler in ContextHandlers)
               {
                    var result = handler(profile, _mockContext);

                    if (result != null)
                    {
                         return result;
                    }
               }

               throw new InvalidOperationException($"Cannot find context for service profile type '{profile.GetType().Name}'.");
          }

          private static readonly List<Func<IServiceProfile, IMockContext, IServiceContext>> ContextHandlers = new List<Func<IServiceProfile, IMockContext, IServiceContext>>
          {
               // ReSharper disable MergeCastWithTypeCheck
               (profile, context) => (profile is IContainerProfile) ? new ContainerContext((IContainerProfile) profile, context) : null,
               (profile, context) => (profile is ILightweightProfile) ? new LightweightContext((ILightweightProfile) profile, context) : null
          };

          private readonly MockContext _mockContext;

          private readonly bool _refreshMocks;

          private readonly bool _reuseServices;

          private readonly IServiceContext _serviceContext;

          private bool _disposed;
     }
}