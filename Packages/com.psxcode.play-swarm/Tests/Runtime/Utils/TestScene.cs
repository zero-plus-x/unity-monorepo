using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Swarm.RuntimeTests.Utils {
  public static class TestScene {
    public static IEnumerator Load(string sceneName) {
      // Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
      yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
      SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
    }

    public static IEnumerator Unload() {
      yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
      // Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
    }

    public static IEnumerator Run(string sceneName, Func<IEnumerator> runner) {
      yield return Load(sceneName);

      yield return runner();

      yield return Unload();
    }
  }
}
