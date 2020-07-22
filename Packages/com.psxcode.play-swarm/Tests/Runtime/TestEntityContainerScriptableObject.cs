using System.Collections.Generic;
using UnityEngine;

namespace Swarm.RuntimeTests {
  [CreateAssetMenu(menuName = "Create TestEntityContainer", fileName = "TestEntityContainer", order = 0)]
  internal class TestEntityContainerScriptableObject : ScriptableObject, IEntityContainerProducer, IServiceProducer {
    readonly TestEntityContainer container = new TestEntityContainer();

    public IEnumerable<IEntityContainer> GetEntityContainers() {
      yield return container;
    }

    public IEnumerable<IServiceInstance> GetServices() {
      yield return container;
    }
  }
}
