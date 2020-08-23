using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Swarm.RuntimeTests {
  public class ServicesHolder_Tests {
    [UnityTest]
    public IEnumerator TestSceneLoading() {
      Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
      yield return SceneManager.LoadSceneAsync("TestInjector", LoadSceneMode.Additive);

      SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.sceneCount - 1));
      yield return null;
      yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
      Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
    }
  }
}
