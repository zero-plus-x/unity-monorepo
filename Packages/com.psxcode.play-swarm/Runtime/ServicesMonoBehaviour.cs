using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Swarm {
  public class ServicesMonoBehaviour : MonoBehaviour {
    [SerializeField]
    public List<ScriptableObject> services;

    [SerializeField]
    public List<ScriptableObject> functions = new List<ScriptableObject>();

    void Start() {
      SimpleServiceContainer.Create(
        services.OfType<IServiceProducer>(),
        functions.OfType<IGetTypesToResolve>()
      );
    }
  }
}
