namespace ContainerTests2.Contracts
{
     public interface IContextFactory
     {
          IServiceContext GetContext(IServiceProfile profile);
     }
}