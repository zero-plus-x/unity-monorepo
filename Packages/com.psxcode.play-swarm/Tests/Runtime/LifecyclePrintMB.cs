using System;
using UnityEngine;

namespace Swarm.RuntimeTests {
  internal class LifecyclePrintMB : MonoBehaviour {

    void Awake() {
      Debug.Log("Awake was called");
    }

    void OnEnable() {
      Debug.Log("OnEnable was called");
    }

    void OnDisable() {
      Debug.Log("OnDisable was called");
    }

    void OnDestroy() {
      Debug.Log("OnDestroy was called");
    }

    void Start() {
      Debug.Log("Start was called");
    }

    void Update() {
      Debug.Log("Update was called");
    }
  }
}
