namespace ContainerTests.Contracts
{
     public interface ILightweightProfile : IServiceProfile
     {
          void RegisterServices(IServiceBuilder serviceBuilder, IMockResolver mockResolver);
     }
}