using Moq;

namespace ContainerTests2.Contracts
{
     public interface IMockRegistration
     {
          bool CanRefresh { get; }

          Mock Mock { get; }

          void Refresh();
     }
}