using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Swarm.Common {
  internal static class AssemblyExtensions {
    public static IEnumerable<Assembly> WhereAssemblyName(this IEnumerable<Assembly> @this, string name) =>
      @this.Where(a => a.GetName().Name == name);

    public static IEnumerable<Assembly> GetDomainAssemblies() => AppDomain.CurrentDomain.GetAssemblies();

    public static IEnumerable<Type> GetAssembliesTypes(this IEnumerable<Assembly> @this) =>
      @this.SelectMany(a => a.GetTypes()).Where(t => t.IsClass && !t.IsAbstract && !t.IsInterface);
  }
}
