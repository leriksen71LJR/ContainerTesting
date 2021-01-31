using System;
using ContainerTests.Contracts;

namespace ContainerTests.Models
{
     public class LightweightProfile : ILightweightProfile
     {
          public LightweightProfile(Action<IServiceBuilder, IMockResolver> registerServices)
          {
               if (registerServices == null) throw new ArgumentNullException(nameof(registerServices));

               RegisterServices = registerServices ?? throw new ArgumentNullException(nameof(registerServices));
          }

          private Action<IServiceBuilder, IMockResolver> RegisterServices { get; }

          void ILightweightProfile.RegisterServices(IServiceBuilder serviceBuilder, IMockResolver mockResolver)
          {
               RegisterServices?.Invoke(serviceBuilder, mockResolver);
          }
     }
}
