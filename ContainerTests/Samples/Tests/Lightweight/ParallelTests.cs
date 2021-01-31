using System;
using System.Threading;
using ContainerTests.Implementation;
using ContainerTests.Models;
using ContainerTests.Samples.Contracts;
using ContainerTests.Samples.Implementations;
using FluentAssertions;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ContainerTests.Samples.Tests.Lightweight
{
     public class ParallelTests : ParallelTestFixture
     {
          public ParallelTests() : base(TestProfile)
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

          private static readonly TestingProfile TestProfile = new TestingProfile(
               mocks: new MockProfile(builder =>
                    {
                         builder.Register<ITestMock1>(configureAction: mock =>
                         {
                              mock.Setup(m => m.GetValue()).Returns(() => DateTime.Now.ToString());
                         });
                    }),
               services: new LightweightProfile((builder, resolver) =>
                    {
                         builder.Register(new TestService1(resolver.GetMock<ITestMock1>().Object));
                    })
          );
     }
}