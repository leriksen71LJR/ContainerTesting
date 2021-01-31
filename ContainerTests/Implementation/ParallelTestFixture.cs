using System;
using ContainerTests.Contracts;
using NUnit.Framework;

namespace ContainerTests.Implementation
{
     [Parallelizable(ParallelScope.All)]
     [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
     public abstract class ParallelTestFixture : BaseTestFixture
     {
          protected ParallelTestFixture(ITestingProfile testingProfile) : base(testingProfile)
          {
          }

          protected ParallelTestFixture(Func<ITestingProfile> profileFactory) : base(profileFactory)
          {
          }

          [TearDown]
          public void ParallelTestTearDown()
          {
               DisposeFixture();
          }

     }
}
