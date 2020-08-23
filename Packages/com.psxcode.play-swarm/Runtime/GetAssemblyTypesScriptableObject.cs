using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Swarm {
  using Common;

  [CreateAssetMenu(menuName = "Create GetAssemblyTypes", fileName = "GetAssemblyTypes", order = 0)]
  public class GetAssemblyTypesScriptableObject : ScriptableObject, IGetTypesToResolve {
    static bool NotNull(string value) => !string.IsNullOrWhiteSpace(value);

    static IEnumerable<Type> GetAssemblyTypes(string name) =>
      AssemblyExtensions
        .GetDomainAssemblies()
        .WhereAssemblyName(name)
        .GetAssembliesTypes();

    [SerializeField]
    public List<string> assemblyNames = new List<string>();

    [SerializeField]
    public List<string> includeTypeNames = new List<string>();

    [SerializeField]
    public List<string> excludeTypeNames = new List<string>();

    public IEnumerable<Type> GetTypes() =>
      assemblyNames
        .Where(NotNull)
        .SelectMany(GetAssemblyTypes)
        .IncludeTypeNames(includeTypeNames)
        .ExcludeTypeNames(excludeTypeNames);
  }
}
