using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Swarm.Common {
  internal static class Extensions {
    public static void SwapValues<T>(this T[] source, uint index1, uint index2) {
      var temp = source[index1];
      source[index1] = source[index2];
      source[index2] = temp;
    }

    public static void ShiftSwap<T>(this T[] source, uint index1, uint index2) {
      if (index2 < index1) {
        source.ShiftSwap(index2, index1);
        return;
      }

      for (uint i = index1; i < index2; ++i) {
        source.SwapValues(i, i + 1);
      }
    }

    public static int FindIndex<T>(this T[] source, T value) where T : class {
      for (var i = 0; i < source.Length; ++i) {
        if (ReferenceEquals(value, source[i])) {
          return i;
        }
      }

      return -1;
    }

    public static void Exec<T>(this IEnumerable<T> @this, Action<T> act) {
      foreach (var element in @this) {
        act(element);
      }
    }

    public static IEnumerable<T> Do<T>(this IEnumerable<T> @this, Action<T> act) {
      foreach (var element in @this) {
        act(element);
        yield return element;
      }
    }

    public static IEnumerable<T> Log<T, R>(this IEnumerable<T> @this, Func<T, R> xf) =>
      @this.Do(x => Debug.Log(xf(x)));

    public static IEnumerable<T> DistinctInstances<T>(this IEnumerable<T> @this) where T : class =>
      @this.Distinct(new ReferenceEqualityComparer<T>());

    static FieldInfo GetBackingField(this PropertyInfo @this) =>
      @this.DeclaringType
        .GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static)
        .First(field =>
          field.Attributes.HasFlag(FieldAttributes.Private | FieldAttributes.InitOnly) &&
          // field.CustomAttributes.Any(attr => attr.AttributeType == typeof(CompilerGeneratedAttribute)) &&
          // (field.DeclaringType == @this.DeclaringType) &&
          field.FieldType.IsAssignableFrom(@this.PropertyType) &&
          field.Name.StartsWith("<" + @this.Name + ">")
        );

    public static void SetValueForce(this PropertyInfo @this, object instance, object value) {
      var setter = @this.GetSetMethod(true);

      if (setter == null) {
        @this.GetBackingField().SetValue(instance, value);
      }
      else {
        setter.Invoke(instance, new[] {value});
      }
    }

    public static IEnumerable<Type> ExcludeTypeNames(this IEnumerable<Type> @this, IEnumerable<string> excludeNames) {
      var namesSet = new HashSet<string>(excludeNames);

      return namesSet.Count == 0
        ? @this
        : @this.Where(type => !namesSet.Contains(type.Name));
    }

    public static IEnumerable<Type> IncludeTypeNames(this IEnumerable<Type> @this, IEnumerable<string> includeNames) {
      var namesSet = new HashSet<string>(includeNames);

      return namesSet.Count == 0
        ? @this
        : @this.Where(type => namesSet.Contains(type.Name));
    }
  }
}
