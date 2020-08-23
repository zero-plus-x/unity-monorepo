using System;
using System.Collections.Generic;
using UnityEngine;

namespace Swarm {
  public class SimpleTypeEntityMap : ITypeEntityMap {
    public static ITypeEntityMap Create() =>
      new SimpleTypeEntityMap(
        () => new Dictionary<Type, ISet<Component>>(),
        () => new HashSet<Component>()
      );

    readonly IDictionary<Type, ISet<Component>> map;
    readonly Func<ISet<Component>> CreateNewSet;

    SimpleTypeEntityMap(
      Func<IDictionary<Type, ISet<Component>>> createNewMap,
      Func<ISet<Component>> createNewSet
    ) {
      map = createNewMap();
      CreateNewSet = createNewSet;
    }

    public void Add(Type type, Component instance) {
      if (map.TryGetValue(type, out var set)) {
        set.Add(instance);
      }
      else {
        var newSet = CreateNewSet();
        newSet.Add(instance);

        map.Add(type, newSet);
      }
    }

    public void Remove(Component instance) {
      foreach (var set in map.Values) {
        set.Remove(instance);
      }
    }

    public IEnumerable<Component> Get(Type type) {
      return map[type];
    }
  }
}
