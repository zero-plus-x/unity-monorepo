using System.Collections.Generic;
using UnityEngine;

namespace Swarm.RuntimeTests {
  [Service]
  internal class TestEntityContainer : IEntityContainer, IServiceInstance {
    public readonly List<Component> entities = new List<Component>();

    public void AddEntity(Component instance) {
      entities.Add(instance);

      Debug.Log($"TestEntityContainer: {instance.name} added");
    }

    public void RemoveEntity(Component instance) {
      Debug.Log($"TestEntityContainer: {instance.name} removed");

      entities.Remove(instance);
    }
  }
}
