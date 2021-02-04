using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContainerTests2.Contracts;
using ContainerTests2.Contracts.Profiles;
using Moq;

namespace ContainerTests2.Implementation.Profiles
{
     public class MockProfile : IMockProfile
     {
          public MockProfile(Action<IMockBuilder> registerMocks)
          {
               if (registerMocks == null) throw new ArgumentNullException(nameof(registerMocks));
               _registerMocks = registerMocks;
          }

          private readonly Action<IMockBuilder> _registerMocks;

          #region explicit interface

          public void RegisterMocks(IMockBuilder builder)
          {
               _registerMocks?.Invoke(builder);
          }

          #endregion
     }
}
