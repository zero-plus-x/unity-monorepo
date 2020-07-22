using System;
using System.Collections.Generic;

namespace Swarm.Common {
  public static class OnceFor {
    public static Action<object> Type(Action<Type> act) {
      var set = new HashSet<Type>();

      return (instance) => {
        var type = instance.GetType();

        if (!set.Contains(type)) {
          set.Add(type);
          act(type);
        }
      };
    }
  }
}
