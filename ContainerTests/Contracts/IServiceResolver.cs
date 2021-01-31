namespace ContainerTests.Contracts
{
     public interface IServiceResolver
     {
          TService GetService<TService>();
     }
}