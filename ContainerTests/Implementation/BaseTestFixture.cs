using System;
using ContainerTests.Contracts;
using Moq;
using NUnit.Framework;

namespace ContainerTests.Implementation
{
     [TestFixture]
     public abstract class BaseTestFixture : IMockResolver, IServiceResolver
     {
          protected BaseTestFixture(ITestingProfile testingProfile)
          {
               if (testingProfile == null) throw new ArgumentNullException(nameof(testingProfile));

               _testingContext = new TestingContext(testingProfile);
          }

          protected BaseTestFixture(Func<ITestingProfile> profileFactory)
          {
               if (profileFactory == null) throw new ArgumentNullException(nameof(profileFactory));

               var testingProfile = profileFactory();

               if (testingProfile == null)
               {
                    throw new ArgumentOutOfRangeException($"Parameter '{profileFactory}' returns mull. An instance of {nameof(ITestingProfile)} is required.");
               }

               _testingContext = new TestingContext(testingProfile);
          }

          public Mock<T> GetMock<T>() where T : class
          {
               return _testingContext.GetMock<T>();
          }

          public TService GetService<TService>()
          {
               return _testingContext.GetService<TService>();
          }

          public void RefreshContext()
          {
               _testingContext.Refresh();
          }

          public void DisposeFixture()
          {
               _testingContext.Dispose();
          }

          private readonly ITestingContext _testingContext;
     }
}