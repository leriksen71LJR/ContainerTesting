namespace ContainerTests2.Contracts.Profiles
{
     public interface ILightweightProfile : IServiceProfile
     {
          void RegisterServices(IServiceBuilder serviceBuilder, IMockResolver mockResolver);
     }
}