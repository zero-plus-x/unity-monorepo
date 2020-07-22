using System.Collections.Generic;

namespace Swarm {
  public interface IEntityContainerProducer {
    IEnumerable<IEntityContainer> GetEntityContainers();
  }
}
