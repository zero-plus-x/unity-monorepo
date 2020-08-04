using System.Collections;
using UnityEngine;

public class TargetFrameRate : MonoBehaviour {
  public int frameRate = 60;

  IEnumerator Start() {
    yield return new WaitForSeconds(1);
    Application.targetFrameRate = frameRate;
  }
}
