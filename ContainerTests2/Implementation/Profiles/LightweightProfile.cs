using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerTests2.Contracts;
using ContainerTests2.Contracts.Profiles;

namespace ContainerTests2.Implementation.Profiles
{
     public class LightweightProfile : ILightweightProfile
     {
          public LightweightProfile(Action<IServiceBuilder, IMockResolver> registerServices)
          {
               if (registerServices == null) throw new ArgumentNullException(nameof(registerServices));

               RegisterServices = registerServices ;
          }

          private Action<IServiceBuilder, IMockResolver> RegisterServices { get; }


          #region explicit implementation

          void ILightweightProfile.RegisterServices(IServiceBuilder serviceBuilder, IMockResolver mockResolver)
          {
               RegisterServices?.Invoke(serviceBuilder, mockResolver);
          }

          #endregion
     }
}
