﻿using ContainerTests2.Contracts;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ContainerTests2.Implementation
{
     public class ServiceBuilder : IServiceBuilder
     {
          public ServiceBuilder()
          {
               _services = new List<object>();
          }

          public IImmutableList<object> GetServices()
          {
               return _services.ToImmutableList();
          }

          public void Register(object service)
          {
               if (service == null)
               {
                    return;
               }

               _services.Add(service);
          }

          private readonly List<object> _services;
     }
}