using System;
using System.Collections.Generic;
using System.Linq;
using Swarm.Common;
using UnityEngine;

namespace Swarm {
  [Service(typeof(IServiceContainer))]
  public class SimpleServiceContainer : IServiceContainer {
    public static SimpleServiceContainer Create(
      IEnumerable<IServiceProducer> services,
      IEnumerable<IGetTypesToResolve> additionalTypesToResolve
    ) => new SimpleServiceContainer(SimpleServiceMap.Create(), services, additionalTypesToResolve);

    public static SimpleServiceContainer Create(IEnumerable<IServiceProducer> services) =>
      new SimpleServiceContainer(SimpleServiceMap.Create(), services, Enumerable.Empty<IGetTypesToResolve>());

    readonly IServiceMap serviceMap;

    SimpleServiceContainer(
      IServiceMap serviceMap,
      IEnumerable<IServiceProducer> services,
      IEnumerable<IGetTypesToResolve> additionalTypesToResolve
    ) {
      this.serviceMap = serviceMap;

      serviceMap.Add(typeof(IServiceContainer), this);

      foreach (var instance in services.GetServiceInstances()) {
        Debug.Assert(instance != null, "SimpleServiceContainer::Ctor: instance is null");

        instance.RegisterServiceTypesTo(serviceMap);
      }

      additionalTypesToResolve
        .GetTypesToResolve()
        // .Log(x => x.Name)
        .Exec(ResolveStatic);
    }

    public void ResolveStatic(Type type) {
      type.GetStaticDependencies()
        .SetDependenciesFrom(serviceMap);
    }

    public void ResolveInstance(object instance) {
      instance.GetType()
        .GetInstanceDependencies()
        .SetDependenciesFrom(serviceMap, instance);
    }
  }
}
