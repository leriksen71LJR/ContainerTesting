namespace ContainerTests.Contracts
{

     public interface ITestingProfile
     {
          bool RefreshMocks { get; }

          bool ReuseServices { get; }

          IMockProfile Mocks { get;  }

          IServiceProfile Services { get;  }
     }
}