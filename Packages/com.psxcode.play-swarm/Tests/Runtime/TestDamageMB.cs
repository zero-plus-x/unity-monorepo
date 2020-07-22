using System.Collections;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Swarm.RuntimeTests {
  [Entity]
  internal class TestDamageMB : MonoBehaviour {
    [UsedImplicitly]
    [RequireService]
    public static IEntityContainer EntityContainer { get; }

    [UsedImplicitly]
    [RequireService]
    public static ITypeEntityContainer TypeContainer { get; }

    void Start() {
      EntityContainer.AddEntity(this);
    }

    public int GetHealthFromEntity() =>
      TypeContainer
        .GetEntities<ITestHealth>()
        .First()
        .Health;

    void OnDestroy() {
      EntityContainer.RemoveEntity(this);
    }
  }
}
