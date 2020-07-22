using UnityEngine;

namespace Swarm {
  public interface IEntityContainer : IServiceInstance {
    void AddEntity(Component instance);
    void RemoveEntity(Component instance);
  }
}
