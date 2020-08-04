using UnityEngine;
using PlayEase;
using PlayTween;

public class TweenTest : MonoBehaviour {
  public GameObject beginObj, endObj;

  readonly Vector3Tween tween = new Vector3Tween();

  void Start() {
    tween.SetDuration(2f);
    tween.SetBeginGetter(beginGetter, true);
    tween.SetEndGetter(endGetter, true);
    tween.SetSetter(setter);
    tween.SetOnComplete(onComplete);
    tween.SetEase(Easing.CubicInOut);
  }

  // Update is called once per frame
  void Update() {
    tween.UpdateAndApply();
  }

  void OnGUI() {
    if (GUILayout.Button("Restart")) {
      tween.Restart();
    }

    if (GUILayout.Button("Pause")) {
      tween.SetEnabled(!tween.IsEnabled);
    }

    if (GUILayout.Button("Reverse")) {
      tween.Reverse();
    }
  }

  Vector3 beginGetter() {
    return beginObj.transform.position;
  }

  Vector3 endGetter() {
    return endObj.transform.position;
  }

  void setter(Vector3 val) {
    transform.position = val;
  }

  void onComplete() {
    Debug.Log("Completed");
  }

  float timeStep() {
    return Time.deltaTime;
  }
}
