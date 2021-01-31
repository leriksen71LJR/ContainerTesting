using System;
using ContainerTests.Contracts;

namespace ContainerTests.Models
{
     public class TestingProfile : ITestingProfile
     {
          public TestingProfile(IMockProfile mocks, IServiceProfile services)
               : this(mocks, services, true, true)
          {
          }

          public TestingProfile(IMockProfile mocks, IServiceProfile services, bool refreshMocks = true,
               bool reuseServices = true)
          {
               Mocks = mocks ?? throw new ArgumentNullException(nameof(mocks));
               Services = services ?? throw new ArgumentNullException(nameof(services));
               RefreshMocks = refreshMocks;
               ReuseServices = reuseServices;
          }

          public IMockProfile Mocks { get; }

          public bool RefreshMocks { get; }

          public bool ReuseServices { get; }

          public IServiceProfile Services { get; }
     }
}