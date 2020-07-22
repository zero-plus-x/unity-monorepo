using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Swarm.Common {
  /// <summary>
  /// An equality comparer that compares objects for reference equality.
  /// </summary>
  /// <typeparam name="T">The type of objects to compare.</typeparam>
  internal sealed class ReferenceEqualityComparer<T> : IEqualityComparer<T>
    where T : class {
    /// <inheritdoc />
    public bool Equals(T left, T right) {
      return object.ReferenceEquals(left, right);
    }

    /// <inheritdoc />
    public int GetHashCode(T value) {
      return RuntimeHelpers.GetHashCode(value);
    }

    public static readonly ReferenceEqualityComparer<T> Instance = new ReferenceEqualityComparer<T>();
  }
}
