using System.Collections.Generic;
using System.Linq;
using Component = UnityEngine.Component;

namespace Swarm {
  [Service(typeof(ITypeEntityContainer))]
  public class SimpleTypeEntityContainer : ITypeEntityContainer, IEntityContainer, IServiceInstance {
    public static SimpleTypeEntityContainer Create() =>
      new SimpleTypeEntityContainer(SimpleTypeEntityMap.Create());

    readonly ITypeEntityMap map;

    SimpleTypeEntityContainer(ITypeEntityMap map) {
      this.map = map;
    }

    public void AddEntity(Component instance) {
      instance.RegisterEntityTo(map);
    }

    public void RemoveEntity(Component instance) {
      map.Remove(instance);
    }

    public IEnumerable<T> GetEntities<T>() {
      return map.Get(typeof(T)).OfType<T>();
    }
  }
}
