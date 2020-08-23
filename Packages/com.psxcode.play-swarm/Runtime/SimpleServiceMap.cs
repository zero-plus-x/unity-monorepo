using System;
using System.Collections.Generic;
using Swarm.Common;

namespace Swarm {
  public class SimpleServiceMap : IServiceMap {
    public static SimpleServiceMap Create() =>
      new SimpleServiceMap(new Dictionary<Type, IServiceInstance>());

    public static SimpleServiceMap Create(IDictionary<Type, IServiceInstance> typeMap) =>
      new SimpleServiceMap(typeMap);

    readonly IDictionary<Type, IServiceInstance> typeMap;

    SimpleServiceMap(IDictionary<Type, IServiceInstance> typeMap) {
      this.typeMap = typeMap;
    }

    public void Add(Type type, IServiceInstance instance) {
      try {
        typeMap.Add(type, instance);
      }
      catch (ArgumentException e) {
        throw new ServiceAlreadyRegisteredException($"Service for type '{type.Name}' already registered", e);
      }
    }

    public IServiceInstance Get(Type type) {
      try {
        return typeMap[type];
      }
      catch (KeyNotFoundException e) {
        throw new ServiceNotFoundException($"Service for type '{type.Name}' was not found", e);
      }
    }
  }
}
