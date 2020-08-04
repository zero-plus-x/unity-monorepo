using System;

namespace PlayTween {
  public class Timer {
    const float MIN_DURATION = 0.001f;

    public bool IsEnabled { get; private set; } = false;

    public bool IsLooping { get; private set; } = false;

    public bool IsStarted { get; private set; } = false;

    public float Duration { get; private set; } = 1f;

    public float TimePassed { get; private set; } = 0f;

    public float TimePassedNorm => TimePassed / Duration;

    public float TimeRemaining => Duration - TimePassed;

    public float TimeRemainingNorm => (Duration - TimePassed) / Duration;

    public bool NearEnd => (Duration - TimePassed) < MIN_DURATION;

    public bool NearBegin => TimePassed < MIN_DURATION;

    public float TimeScale { get; private set; } = 1f;

    bool autoupdate = false;

    Func<float> time_step_getter;
    Func<float> duration_getter;
    Action update_func;

    Action complete_event;
    Action complete_once_event;

    public Timer() {
      time_step_getter = _defaultTimestepGetter;
      update_func = _updateInactive;
      IsStarted = IsEnabled = false;
    }

    public void Update() {
      update_func();
    }

    public void Reset() {
      Reset(duration_getter?.Invoke() ?? Duration);
    }

    public void Reset(float time) {
      SetDuration(time);
      TimePassed = 0f;

      if (autoupdate) {
        TweenRuntime.Instance.AddUpdateOnMT(Update);
      }

      update_func = _update;
      IsEnabled = IsStarted = true;
    }

    public void SetTimePassed(float timePassed) {
      TimePassed = (timePassed < 0f) ? 0f : (timePassed > Duration) ? Duration : timePassed;
    }

    public void SetTimePassedNorm(float timePassedNorm) {
      SetTimePassed(timePassedNorm * Duration);
    }

    public void SetLooping(bool looping) {
      IsLooping = looping;
    }

    public void Complete(bool callEvents) {
      if (!IsStarted) return;

      if (callEvents) {
        if (complete_once_event != null) {
          TweenRuntime.Instance.InvokeOnMT(complete_once_event);
          complete_once_event = null;
        }

        if (complete_event != null) {
          TweenRuntime.Instance.InvokeOnMT(complete_event);
        }
      }

      if (IsLooping) {
        Reset();
      }
      else {
        if (autoupdate) {
          TweenRuntime.Instance.RemoveUpdateOnMT(Update);
        }

        TimePassed = Duration;
        update_func = _updateInactive;
        IsStarted = IsEnabled = false;
      }
    }

    public void SetEnabled(bool enabled) {
      if (IsStarted) {
        IsEnabled = enabled;
        if ((enabled)) update_func = _update;
        else update_func = _updateInactive;
      }
    }

    public void SetAutoUpdate(bool enabled) {
      if (enabled) {
        if (IsStarted) {
          TweenRuntime.Instance.AddUpdateOnMT(Update);
        }
      }
      else {
        if (autoupdate) {
          TweenRuntime.Instance.RemoveUpdateOnMT(Update);
        }
      }

      autoupdate = enabled;
    }

    public void SetOnComplete(Action func) {
      complete_event = func;
    }

    public void AddOnComplete(Action func) {
      complete_event += func;
    }

    public void RemoveOnComplete(Action func) {
      complete_event -= func;
    }

    public void SetOnCompleteOnce(Action func) {
      complete_once_event = func;
    }

    public void AddOnCompleteOnce(Action func) {
      complete_once_event += func;
    }

    public void RemoveOnCompleteOnce(Action func) {
      complete_once_event -= func;
    }

    public void SetTimeStepGetter(Func<float> timeStepGetter) {
      time_step_getter = timeStepGetter ?? _defaultTimestepGetter;
    }

    public void SetDuration(float durationTime) {
      Duration = (durationTime < MIN_DURATION) ? MIN_DURATION : durationTime;
    }

    public void SetDurationGetter(Func<float> durationGetter) {
      duration_getter = durationGetter;
    }

    public void SetTimeScale(float timeScale) {
      TimeScale = (timeScale < 0f) ? 0f : timeScale;
    }

    public void ClearEvents() {
      complete_event = null;
      complete_once_event = null;
    }

    static float _defaultTimestepGetter() {
      return UnityEngine.Time.deltaTime;
    }

    void _update() {
      if ((TimePassed += (time_step_getter() * TimeScale)) > Duration) {
        Complete(true);
      }
    }

    void _updateInactive() { }
  }
}
