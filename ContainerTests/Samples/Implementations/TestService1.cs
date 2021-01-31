using ContainerTests.Samples.Contracts;

namespace ContainerTests.Samples.Implementations
{
     public class TestService1 : ITestService1
     {
          public TestService1(ITestMock1 testMock)
          {
               this.testMock = testMock;
          }

          public string ShowValue()
          {
               return $"Value: {testMock.GetValue()}";
          }

          private readonly ITestMock1 testMock;
     }
}