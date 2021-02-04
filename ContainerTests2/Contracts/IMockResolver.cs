using Moq;

namespace ContainerTests2.Contracts
{
     public interface IMockResolver
     {
          Mock<T> GetMock<T>() where T : class;
     }
}