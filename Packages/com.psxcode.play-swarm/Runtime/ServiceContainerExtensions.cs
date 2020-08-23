using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Swarm.Common;
using UnityEngine;

namespace Swarm {
  internal static class ServiceContainerExtensions {
    static bool HasDependencyAttribute(PropertyInfo info) => info.IsDefined(typeof(RequireServiceAttribute), false);

    public static IEnumerable<PropertyInfo> GetStaticDependencies(this Type @this) =>
      @this
        .GetProperties(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy)
        .Where(HasDependencyAttribute);

    public static IEnumerable<PropertyInfo> GetInstanceDependencies(this Type @this) =>
      @this
        .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
        .Where(HasDependencyAttribute);

    public static void SetDependenciesFrom(
      this IEnumerable<PropertyInfo> @this,
      IServiceMap serviceMap,
      object instance = null
    ) {
      foreach (var info in @this) {
        info.SetValueForce(instance, serviceMap.Get(info.PropertyType));
      }
    }

    static IEnumerable<Type> GetServiceInstanceTypesToRegister(this IServiceInstance @this) {
      var actualType = @this.GetType();

      return actualType
        .GetCustomAttributes(typeof(ServiceAttribute), false)
        .Select(x => ((ServiceAttribute) x).Type ?? actualType);
    }

    public static void RegisterServiceTypesTo(this IServiceInstance @this, IServiceMap serviceMap) {
      foreach (var type in @this.GetServiceInstanceTypesToRegister()) {
        serviceMap.Add(type, @this);
      }
    }

    public static IEnumerable<IServiceInstance> GetServiceInstances(this IEnumerable<IServiceProducer> services) =>
      services.SelectMany(s => s.GetServices());

    public static IEnumerable<Type> GetTypesToResolve(this IEnumerable<IGetTypesToResolve> @this) =>
      @this.SelectMany(x => x.GetTypes());
  }
}
