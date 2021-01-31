using System;
using ContainerTests.Contracts;
using NUnit.Framework;

namespace ContainerTests.Implementation
{
     [Parallelizable(ParallelScope.None)]
     public abstract class SequentialTestFixture : BaseTestFixture
     {
          protected SequentialTestFixture(ITestingProfile testingProfile) : base(testingProfile)
          {
          }

          protected SequentialTestFixture(Func<ITestingProfile> profileFactory) : base(profileFactory)
          {
          }


          [OneTimeTearDown]
          public void OneTimeTearDown()
          {
               DisposeFixture();
          }

          [TearDown]
          public void TearDown()
          {
               RefreshContext();
          }
     }
}