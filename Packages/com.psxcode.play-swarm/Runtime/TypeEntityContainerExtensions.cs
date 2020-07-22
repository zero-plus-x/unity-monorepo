using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Swarm {
  internal static class TypeEntityContainerExtensions {
    static IEnumerable<Type> GetEntityTypesToAdd(this Component @this) {
      // Result can be memoized
      var actualType = @this.GetType();

      return actualType
        .GetCustomAttributes(typeof(EntityAttribute), false)
        .Select(x => ((EntityAttribute) x).Type ?? actualType);
    }

    public static void RegisterEntityTo(this Component instance, ITypeEntityMap map) {
      foreach (var type in instance.GetEntityTypesToAdd()) {
        map.Add(type, instance);
      }
    }
  }
}
