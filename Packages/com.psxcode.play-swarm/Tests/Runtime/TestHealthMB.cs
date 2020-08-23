using JetBrains.Annotations;
using UnityEngine;

namespace Swarm.RuntimeTests {
  internal interface ITestHealth {
    int Health { get; }
  }

  [Entity(typeof(ITestHealth))]
  internal class TestHealthMB : MonoBehaviour, ITestHealth {
    [UsedImplicitly]
    [RequireService]
    public static IEntityContainer EntityContainer { get; }

    [SerializeField]
    public int health;

    public int Health => health;

    void Start() {
      EntityContainer.AddEntity(this);
    }

    void OnDestroy() {
      EntityContainer.RemoveEntity(this);
    }
  }
}
