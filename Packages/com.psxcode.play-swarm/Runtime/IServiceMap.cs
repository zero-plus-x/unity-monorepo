using System;

namespace Swarm {
  public interface IServiceMap {
    void Add(Type type, IServiceInstance instance);
    IServiceInstance Get(Type type);
  }
}
