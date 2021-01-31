using System;
using Autofac;
using ContainerTests.Implementation;
using ContainerTests.Models;
using ContainerTests.Samples.Contracts;
using ContainerTests.Samples.Implementations;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace ContainerTests.Samples.Tests.Lightweight
{
     public class SequentialTests : SequentialTestFixture
     {
          public SequentialTests() : base(GetProfile)
          {
          }

          [Test]
          public void Get_TestExecutionContext()
          {
               var context = TestExecutionContext.CurrentContext;

               context.Should().NotBeNull();
          }

          [Test]
          public void Mock_Default_NotNull()
          {
               var mock = GetMock<ITestMock1>();

               mock.Should().NotBeNull();
          }

          [Test]
          public void Mock_Reset_HasDifferentCountBeforeAndAfter()
          {
               var mock = GetMock<ITestMock1>();
               mock.Setup(m => m.GetValue()).Returns(() => "Test1");

               mock.Setups.Count.Should().Be(1);

               mock.Reset();

               mock.Setups.Count.Should().Be(0);
          }

          [Test]
          public void Service_Default_NotNull()
          {
               var service = GetService<ITestService1>();

               service.Should().NotBeNull();
          }

          [Test]
          public void Service_ShowValue_HasMockValue()
          {
               var mockValue = Guid.NewGuid().ToString();

               var mock = GetMock<ITestMock1>();
               mock.Setup(m => m.GetValue()).Returns(() => mockValue);

               var service = GetService<ITestService1>();

               var result = service.ShowValue();

               result.Should().NotBeNullOrEmpty();

               result.Should().Contain(mockValue);

               mock.Verify(m => m.GetValue(), Times.Once);

               Console.WriteLine(result);
          }

          [Test]
          public void Service_ShowValue_NotNull()
          {
               var service = GetService<ITestService1>();

               var result = service.ShowValue();

               result.Should().NotBeNullOrEmpty();
          }

          [Test]
          public void Service_ShowValue_VerifyOnce()
          {
               var mock = GetMock<ITestMock1>();
               mock.Setup(m => m.GetValue()).Returns(() => "test1");

               var service = GetService<ITestService1>();

               var result = service.ShowValue();

               result.Should().NotBeNullOrEmpty();

               mock.Verify(m => m.GetValue(), Times.Once);
          }

          private static TestingProfile GetProfile()
          {
               return new TestingProfile(
                    mocks: new MockProfile(builder => { builder.Register(new MockRegistration<ITestMock1>()); }),
                    services: new ContainerProfile(builder => { },
                         builder =>
                         {
                              builder.RegisterType<TestService1>()
                                   .As<ITestService1>();
                         })
               );
          }
     }
}