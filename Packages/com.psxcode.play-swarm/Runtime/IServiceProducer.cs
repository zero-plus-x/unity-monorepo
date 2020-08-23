using System.Collections.Generic;

namespace Swarm {
  public interface IServiceProducer {
    IEnumerable<IServiceInstance> GetServices();
  }
}
