using Moq;

namespace ContainerTests.Contracts
{
     public interface IMockResolver
     {
          Mock<T> GetMock<T>() where T : class;
     }
}