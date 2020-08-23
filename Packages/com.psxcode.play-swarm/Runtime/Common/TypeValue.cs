using System;

namespace Swarm.Common {
  internal readonly struct TypeValue<T> where T : class {
    public readonly Type type;
    public readonly T value;

    public TypeValue(T value) {
      this.value = value;
      this.type = value.GetType();
    }
  }

  internal static class TypeValue {
    public static TypeValue<T> Create<T>(T value) where T : class => new TypeValue<T>(value);
  }
}
