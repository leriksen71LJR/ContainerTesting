using Moq;

namespace ContainerTests.Contracts
{
     public interface IMockRegistration
     {
          Mock Mock { get; }

          bool CanRefresh { get; }

          void Refresh();
     }
}