using System.Collections.Immutable;

namespace ContainerTests.Contracts
{
     public interface IMockProvider
     {
          IImmutableList<IMockRegistration> GetRegistrations();
     }
}