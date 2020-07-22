using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Swarm.RuntimeTests {
  using Utils;

  public class InjectorSceneTests {
    [UnityTest]
    public IEnumerator CheckEntityContainerPassesEntities() {
      yield return TestScene.Load("TestInjector");

      // Call Start
      yield return null;

      var healthMb = Object.FindObjectOfType<TestHealthMB>();
      var damageMb = Object.FindObjectOfType<TestDamageMB>();

      Assert.AreEqual(
        damageMb.GetHealthFromEntity(),
        healthMb.Health,
        "Should have equal data"
      );

      yield return TestScene.Unload();
    }
  }
}
