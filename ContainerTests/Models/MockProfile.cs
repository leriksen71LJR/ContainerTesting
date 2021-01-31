using System;
using ContainerTests.Contracts;

namespace ContainerTests.Models
{
     public class MockProfile : IMockProfile
     {
          public MockProfile(Action<IMockBuilder> registerMocks)
          {
               _registerMocks = registerMocks ?? throw new ArgumentNullException(nameof(registerMocks));
          }

          private readonly Action<IMockBuilder> _registerMocks;

          #region interface explicit

          void IMockProfile.RegisterMocks(IMockBuilder builder)
          {
               _registerMocks?.Invoke(builder);
          }

          #endregion

     }
}