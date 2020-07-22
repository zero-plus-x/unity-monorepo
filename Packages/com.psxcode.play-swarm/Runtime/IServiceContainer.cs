using System;

namespace Swarm {
  public interface IServiceContainer : IServiceInstance {
    void ResolveStatic(Type type);
    void ResolveInstance(object instance);
  }
}
