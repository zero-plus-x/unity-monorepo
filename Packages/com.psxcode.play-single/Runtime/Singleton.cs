using UnityEngine;

namespace PlaySingle {
  public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    static T _instance;
    static readonly object _lock = new object();

    public static T Instance {
      get {
        if (applicationIsQuitting) {
#if UNITY_EDITOR
          Debug.LogWarning("Instance request after OnDestroy");
#endif
          return null;
        }

        if (_instance == null) {
          lock (_lock) {
            _instance = (T) FindObjectOfType(typeof(T));
#if UNITY_EDITOR
            if (FindObjectsOfType(typeof(T)).Length > 1) {
              Debug.LogError("Multiple Singletons Detected");
            }
#endif

            if (_instance == null) {
              var singleton = new GameObject();
              _instance = singleton.AddComponent<T>();
              singleton.name = typeof(T).ToString() + "_singleton";

              DontDestroyOnLoad(singleton);
#if UNITY_EDITOR
              Debug.Log("Singleton: " + singleton.name + " created");
#endif
            }
          } //lock
        } //if null

        return _instance;
      }
    }

    static bool applicationIsQuitting;

    void OnDestroy() {
      applicationIsQuitting = true;
    }
  }
}
