using System;
using PlaySingle;

namespace PlayTween {
  public class TweenRuntime : SingletonMonoBehaviour<TweenRuntime> {
    readonly Action[] invoke_on_mt = new Action[2];
    int invoke_cursor = 0;

    public void InvokeOnMT(Action func) {
      invoke_on_mt[invoke_cursor] += func;
    }

    Action update_on_mt;
    Action update_on_mt_playing;

    public void AddUpdateOnMT(Action func) {
      update_on_mt -= func;
      update_on_mt += func;
    }

    public void RemoveUpdateOnMT(Action func) {
      update_on_mt -= func;
    }

    public void EnableUpdateOnMT(Action func, bool enable) {
      update_on_mt -= func;
      if (enable) update_on_mt += func;
    }

    public void AddUpdateOnMT_Playing(Action func) {
      update_on_mt_playing -= func;
      update_on_mt_playing += func;
    }

    public void RemoveUpdateOnMT_Playing(Action func) {
      update_on_mt_playing -= func;
    }

    public void EnableUpdateOnMT_Playing(Action func, bool enable) {
      update_on_mt_playing -= func;
      if (enable) update_on_mt_playing += func;
    }

    void Update() {
      if (invoke_on_mt[invoke_cursor] != null) {
        //store old cursor
        int old_cursor = invoke_cursor;
        //switch to next to prevent add to existing invoke delegate
        invoke_cursor = 1 - invoke_cursor;
        invoke_on_mt[old_cursor]();
        invoke_on_mt[old_cursor] = null;
      }

      update_on_mt?.Invoke();
    }
  }
}
