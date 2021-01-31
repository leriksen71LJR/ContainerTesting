namespace ContainerTests.Contracts
{
     public interface IMockContext : IMockResolver, IMockProvider
     {
          void Build();
          void Refresh();
     }
}