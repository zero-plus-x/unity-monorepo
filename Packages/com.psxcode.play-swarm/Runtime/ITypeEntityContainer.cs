using System.Collections.Generic;

namespace Swarm {
  public interface ITypeEntityContainer {
    IEnumerable<T> GetEntities<T>();
  }
}
