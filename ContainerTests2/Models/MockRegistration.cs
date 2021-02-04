using ContainerTests2.Contracts;
using Moq;
using System;

namespace ContainerTests2.Models
{
     public class MockRegistration<T> : IMockRegistration
          where T : class
     {
          public MockRegistration()
               : this(true)
          {
          }

          public MockRegistration(
               bool canRefresh = true,
               MockBehavior behavior = MockBehavior.Loose,
               Action<Mock<T>> configureAction = null,
               Func<T> factory = null)
               : this(factory != null
                    ? new Mock<T>(factory, behavior)
                    : new Mock<T>(behavior))
          {
               Configure = configureAction;
               CanRefresh = canRefresh;

               Refresh();
          }

          private MockRegistration(Mock<T> mock)
          {
               Mock = mock;
          }

          public bool CanRefresh { get; }

          public Action<Mock<T>> Configure { get; }

          public Mock<T> Mock { get; }

          public void Refresh()
          {
               if (!CanRefresh)
               {
                    return;
               }

               Mock.Reset();

               Configure?.Invoke(Mock);
          }

          void IMockRegistration.Refresh()
          {
               Refresh();
          }

          #region interface explicit

          bool IMockRegistration.CanRefresh => CanRefresh;

          Mock IMockRegistration.Mock => Mock;

          public static implicit operator MockRegistration<T>(Mock<T> mock)
          {
               return new MockRegistration<T>(mock);
          }

          #endregion interface explicit
     }
}