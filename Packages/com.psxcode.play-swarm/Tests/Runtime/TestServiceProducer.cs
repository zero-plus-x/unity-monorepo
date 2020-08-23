using System.Collections.Generic;

namespace Swarm.RuntimeTests {
  internal class TestServiceProducer : IServiceProducer {
    readonly IEnumerable<IServiceInstance> instances;

    public TestServiceProducer(IEnumerable<IServiceInstance> instances) {
      this.instances = instances;
    }

    public IEnumerable<IServiceInstance> GetServices() {
      return instances;
    }
  }
}
