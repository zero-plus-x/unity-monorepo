using System;
using System.Collections.Generic;

namespace Swarm {
  public interface IGetTypesToResolve {
    IEnumerable<Type> GetTypes();
  }
}
