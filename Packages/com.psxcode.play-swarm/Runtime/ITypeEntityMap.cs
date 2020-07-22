using System;
using System.Collections.Generic;
using UnityEngine;

namespace Swarm {
  public interface ITypeEntityMap {
    void Add(Type type, Component instance);
    void Remove(Component instance);
    IEnumerable<Component> Get(Type type);
  }
}
