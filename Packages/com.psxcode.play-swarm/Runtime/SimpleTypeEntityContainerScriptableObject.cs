using System;
using System.Collections.Generic;
using UnityEngine;

namespace Swarm {
  [CreateAssetMenu(menuName = "Create SimpleTypeEntityContainer", fileName = "SimpleTypeEntityContainer", order = 0)]
  public class
    SimpleTypeEntityContainerScriptableObject : ScriptableObject, IServiceProducer, IEntityContainerProducer {
    [NonSerialized]
    readonly SimpleTypeEntityContainer container = SimpleTypeEntityContainer.Create();

    public IEnumerable<IServiceInstance> GetServices() {
      yield return container;
    }

    public IEnumerable<IEntityContainer> GetEntityContainers() {
      yield return container;
    }
  }
}
