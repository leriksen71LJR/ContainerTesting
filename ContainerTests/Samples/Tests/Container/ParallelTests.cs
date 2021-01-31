using System;
using System.Threading;
using Autofac;
using ContainerTests.Implementation;
using ContainerTests.Models;
using ContainerTests.Samples.Contracts;
using ContainerTests.Samples.Implementations;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ContainerTests.Samples.Tests.Container
{
     public class ParallelTests : ParallelTestFixture
     {
          public ParallelTests() : base(new Profile())
          {
          }

          [Test]
          public void Mock_Default_NotNull()
          {
               var mock = GetMock<ITestMock1>();

               mock.Should().NotBeNull();
          }

          [Test]
          [TestCase("1")]
          [TestCase("2")]
          [TestCase("3")]
          [TestCase("4")]
          [TestCase("5")]
          public void SimulatedLongTest(string value)
          {
               var context = TestExecutionContext.CurrentContext;

               Console.WriteLine($"{value}: Parallel = {context.ParallelScope}");

               var mock = GetMock<ITestMock1>();

               Thread.Sleep(2000);
          }

          private class Profile : TestingProfile
          {
               public Profile() : base(
                    new MockProfile(
                         builder =>
                         {
                              builder.Register(new MockRegistration<ITestMock1>(configureAction: mock =>
                              {
                                   mock.Setup(m => m.GetValue()).Returns(() => DateTime.Now.ToString());
                              }));
                         }),
                    new ContainerProfile(
                         builder => { },
                         builder => { builder.RegisterType<TestService1>().As<ITestService1>(); }))
               {
               }
          }
     }
}