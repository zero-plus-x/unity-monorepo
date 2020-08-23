using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Swarm {
  [Service(typeof(IEntityContainer))]
  public class CompositeEntityContainer : IEntityContainer, IServiceInstance {
    readonly IEnumerable<IEntityContainer> containers;

    public CompositeEntityContainer(IEnumerable<IEntityContainerProducer> producers) {
      containers = producers
        .SelectMany(p => p.GetEntityContainers())
        .ToArray();
    }

    public void AddEntity(Component instance) {
      foreach (var container in containers) {
        container.AddEntity(instance);
      }
    }

    public void RemoveEntity(Component instance) {
      foreach (var container in containers) {
        container.RemoveEntity(instance);
      }
    }
  }
}
