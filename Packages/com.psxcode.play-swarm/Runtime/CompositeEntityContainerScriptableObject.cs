using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Swarm {
  [CreateAssetMenu(menuName = "Create CompositeEntityContainer", fileName = "CompositeEntityContainer", order = 0)]
  public class CompositeEntityContainerScriptableObject : ScriptableObject, IServiceProducer {
    [SerializeField]
    public List<ScriptableObject> entityContainers;

    [NonSerialized]
    IEntityContainer container;

    void Awake() {
      container = new CompositeEntityContainer(entityContainers.OfType<IEntityContainerProducer>());
    }

    public IEnumerable<IServiceInstance> GetServices() {
      yield return container;
    }
  }
}
