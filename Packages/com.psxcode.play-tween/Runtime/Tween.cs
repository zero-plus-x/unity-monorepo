using System;
using PlayEase;

namespace PlayTween {
  public enum TweenAutoUpdateMode {
    NONE,
    UPDATE,
    APPLY
  }

  public abstract class Tween<T> {
    protected T begin_value, end_value, delta_value, current_value;

    public T CurrentValue => current_value;

    protected Func<T> begin_getter = null, end_getter = null;
    protected Action<T> setter;
    protected bool dynamic_begin = false, dynamic_end = false;
    public bool IsDynamic => dynamic_begin || dynamic_end;

    public delegate float EaseFunction(float time);

    protected EaseFunction ease_function = Easing.Linear;
    protected UnityEngine.AnimationCurve ease_curve;
    Func<T> evalget_func;
    Action evalset_func;

    int loop_count = 1;
    public int LoopsCompleted { get; private set; } = 0;

    TweenAutoUpdateMode autoupdate_mode = TweenAutoUpdateMode.NONE;

    Func<float> duration_getter, delay_getter;
    public float Duration { get; private set; } = 1f;

    public float Delay { get; private set; } = 0f;

    public float TimePassed => IsDelaying ? 0f : timer.TimePassed;

    public float TimePassedNorm => IsDelaying ? 0f : timer.TimePassedNorm;

    public bool IsDelaying { get; private set; } = false;

    readonly Timer timer;

    public bool IsEnabled => timer.IsEnabled;

    public bool IsStarted => timer.IsStarted;

    Action complete_event,
      complete_once_event,
      loop_complete_event,
      loop_complete_once_event;

    protected abstract void UpdateCurrentValue(float time, float duration);
    protected abstract void UpdateDeltaValue();


    public Tween() {
      timer = new Timer();
      timer.SetOnComplete(() => _doComplete(true));
      SetEnabled(false);
    }

    public void Restart() {
      LoopsCompleted = 0;
      _doRestart(delay_getter?.Invoke() ?? Delay, duration_getter?.Invoke() ?? Duration);
    }

    public void Restart(float durationTime) {
      LoopsCompleted = 0;
      _doRestart(-1f, durationTime);
    }

    public void Restart(float delayTime, float durationTime) {
      LoopsCompleted = 0;
      _doRestart(delayTime, durationTime);
    }

    public void Reverse() {
      // swap values.
      // use delta here.
      // if tween is active delta will be updated
      delta_value = begin_value;
      begin_value = end_value;
      end_value = delta_value;

      //swap getters
      var temp_getter = begin_getter;
      begin_getter = end_getter;
      end_getter = temp_getter;

      // swap getter bools
      /*
       bool temp_bool = dynamic_begin;
       dynamic_begin = dynamic_end;
       dynamic_end = temp_bool;
      */

      if (timer.IsStarted) {
        //update if working
        if (begin_getter != null) {
          begin_value = begin_getter();
        }

        if (end_getter != null) {
          end_value = end_getter();
        }

        UpdateDeltaValue();

        timer.SetTimePassed(timer.Duration - timer.TimePassed);
      }
    }

    public void SetTimePosition(float timePos) {
      CompleteDelay();
      timer.SetTimePassed(timePos);
    }

    public void SetTimePositionNorm(float timePosNorm) {
      CompleteDelay();
      timer.SetTimePassedNorm(timePosNorm);
    }

    public void CompleteDelay() {
      if (IsDelaying) {
        _doComplete(false);
      }
    }

    public void Complete(bool callEvents) {
      CompleteDelay();
      loop_count = 1;
      timer.Complete(false);
      _doComplete(callEvents);
    }

    public void CompleteAndClear(bool callEvents) {
      if (timer.IsStarted) {
        Complete(callEvents);
      }

      ClearEvents();
      SetGetters(null, false, null, false);
      SetSetter(null);
      SetEase(Easing.Linear);
    }

    public void SetEnabled(bool enabled) {
      timer.SetEnabled(enabled);
      if (timer.IsEnabled) {
        if (IsDelaying) {
          if (dynamic_begin) {
            //dynamic begin
            evalget_func = _evaluateGet_BeginDynamic;
            evalset_func = _evaluateSet_BeginDynamic;
          }
          else {
            //delaying
            evalget_func = _evaluateGet_Delay;
            evalset_func = _evaluateSet_Delay;
          }
        }
        else {
          //active
          evalget_func = _evaluateGet;
          evalset_func = _evaluateSet;
        }
      }
      else if (timer.IsStarted) {
        //pause
        if (IsDynamic) {
          //full dynamic
          evalget_func = _evaluateGet;
          evalset_func = _evaluateSet;
        }
        else {
          //inactive
          evalget_func = _evaluateGet_Inactive;
          evalset_func = _evaluateSet_Inactive;
        }
      }
      else {
        //complete or not started
        if (dynamic_end && LoopsCompleted > 0) {
          //dynamic end
          evalget_func = _evaluateGet_EndDynamic;
          evalset_func = _evaluateSet_EndDynamic;
        }
        else {
          //inactive
          evalget_func = _evaluateGet_Inactive;
          evalset_func = _evaluateSet_Inactive;
        }
      }
    }

    public void SetValues(T beginValue, T endValue) {
      begin_value = beginValue;
      end_value = endValue;
      UpdateDeltaValue();
    }

    public void SetBeginValue(T beginValue) {
      begin_value = beginValue;
      UpdateDeltaValue();
    }

    public void SetEndValue(T endValue) {
      end_value = endValue;
      UpdateDeltaValue();
    }

    public void SetCurrentValue(T currentValue) {
      current_value = currentValue;
    }

    public void SetBeginGetter(Func<T> beginGetter, bool getBeginOnUpdate) {
      begin_getter = beginGetter;
      dynamic_begin = getBeginOnUpdate;
      SetEnabled(timer.IsEnabled);
    }

    public void SetEndGetter(Func<T> endGetter, bool getEndOnUpdate) {
      end_getter = endGetter;
      dynamic_end = getEndOnUpdate;
      SetEnabled(timer.IsEnabled);
    }

    public void SetGetters(
      Func<T> beginGetter,
      bool getBeginOnUpdate,
      Func<T> endGetter,
      bool getEndOnUpdate
    ) {
      begin_getter = beginGetter;
      dynamic_begin = getBeginOnUpdate;
      end_getter = endGetter;
      dynamic_end = getEndOnUpdate;
      SetEnabled(timer.IsEnabled);
    }

    public void SetSetter(Action<T> setterFunc) {
      setter = setterFunc;
    }

    public void AddSetter(Action<T> setterFunc) {
      setter += setterFunc;
    }

    public void RemoveSetter(Action<T> setterFunc) {
      setter -= setterFunc;
    }

    public void SetEase(EaseFunction easeFunc) {
      ease_curve = null;
      ease_function = easeFunc;
    }

    public void SetEase(UnityEngine.AnimationCurve curve) {
      ease_curve = curve;
      ease_function = EvaluateCurve;
    }

    public void SetAutoUpdate(TweenAutoUpdateMode mode) {
      switch (autoupdate_mode) {
        case TweenAutoUpdateMode.UPDATE:
          TweenRuntime.Instance.RemoveUpdateOnMT(Update);
          break;
        case TweenAutoUpdateMode.APPLY:
          TweenRuntime.Instance.RemoveUpdateOnMT(UpdateAndApply);
          break;
      }

      autoupdate_mode = mode;
      switch (autoupdate_mode) {
        case TweenAutoUpdateMode.UPDATE:
          TweenRuntime.Instance.AddUpdateOnMT(Update);
          break;
        case TweenAutoUpdateMode.APPLY:
          TweenRuntime.Instance.AddUpdateOnMT(UpdateAndApply);
          break;
      }
    }

    public void SetTimeStepGetter(Func<float> timeStepFunc) {
      timer.SetTimeStepGetter(timeStepFunc);
    }

    public void SetDuration(float durationTime) {
      if (!IsDelaying) {
        timer.SetDuration(durationTime);
        //get corrected value
        Duration = timer.Duration;
      }
      else {
        //save unchecked time value for now
        Duration = durationTime;
      }
    }

    public void SetDurationGetter(Func<float> durationGetter) {
      duration_getter = durationGetter;
    }

    public void SetDelay(float delayTime) {
      if (IsDelaying) {
        timer.SetDuration(delayTime);
        //get corrected value
        Delay = timer.Duration;
      }
      else {
        //save unchecked time value for now
        Delay = delayTime;
      }
    }

    public void SetDelayGetter(Func<float> delayGetter) {
      delay_getter = delayGetter;
    }

    public void SetLoopCount(int loopCount) {
      loop_count = loopCount;
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

    public void SetOnLoopComplete(Action func) {
      loop_complete_event = func;
    }

    public void AddOnLoopComplete(Action func) {
      loop_complete_event += func;
    }

    public void RemoveOnLoopComplete(Action func) {
      loop_complete_event -= func;
    }

    public void SetOnLoopCompleteOnce(Action func) {
      loop_complete_once_event = func;
    }

    public void AddOnLoopCompleteOnce(Action func) {
      loop_complete_once_event += func;
    }

    public void RemoveOnLoopCompleteOnce(Action func) {
      loop_complete_once_event -= func;
    }

    public void ClearEvents() {
      complete_event = null;
      complete_once_event = null;
      loop_complete_event = null;
      loop_complete_once_event = null;
    }

    public T EvaluateAndGet() {
      return evalget_func();
    }

    public void EvaluateAndSet() {
      setter(evalget_func());
    }

    public void Update() {
      timer.Update();
    }

    public T UpdateAndGet() {
      timer.Update();
      return evalget_func();
    }

    public void UpdateAndApply() {
      timer.Update();
      evalset_func();
    }

    float EvaluateCurve(float t) {
      return ease_curve.Evaluate(t * ease_curve[ease_curve.length - 1].time);
    }

    T _evaluateGet() {
      UpdateCurrentValue(timer.TimePassed, Duration);
      return current_value;
    }

    T _evaluateGet_Inactive() {
      return current_value;
    }

    T _evaluateGet_Delay() {
      //while in delay Do update once
      UpdateCurrentValue(0f, Duration);
      //then change to inactive
      evalget_func = _evaluateGet_Inactive;
      evalset_func = _evaluateSet_Inactive;
      return current_value;
    }

    T _evaluateGet_BeginDynamic() {
      UpdateCurrentValue(0f, Duration);
      return current_value;
    }

    T _evaluateGet_EndDynamic() {
      UpdateCurrentValue(Duration, Duration);
      return current_value;
    }

    void _evaluateSet() {
      UpdateCurrentValue(timer.TimePassed, Duration);
      setter(current_value);
    }

    void _evaluateSet_Inactive() {
      //do not call setter while inactive
    }

    void _evaluateSet_Delay() {
      //while in delay Do update once
      UpdateCurrentValue(0f, Duration);
      //then change to inactive
      evalget_func = _evaluateGet_Inactive;
      evalset_func = _evaluateSet_Inactive;
      setter(current_value);
    }

    void _evaluateSet_BeginDynamic() {
      UpdateCurrentValue(0f, Duration);
      setter(current_value);
    }

    void _evaluateSet_EndDynamic() {
      UpdateCurrentValue(Duration, Duration);
      setter(current_value);
    }

    void _doRestart(float delayTime, float durationTime) {
      Delay = delayTime;
      Duration = durationTime;
      if (Delay > 0f) {
        timer.Reset(Delay);
        IsDelaying = true;
        //get corrected value
        Delay = timer.Duration;
      }
      else {
        timer.Reset(Duration);
        IsDelaying = false;
        //get corrected value
        Duration = timer.Duration;
      }

      if (dynamic_begin && IsDelaying) {
        evalget_func = _evaluateGet_BeginDynamic;
        evalset_func = _evaluateSet_BeginDynamic;
      }
      else if (IsDelaying) {
        evalget_func = _evaluateGet_Delay;
        evalset_func = _evaluateSet_Delay;
      }
      else {
        evalget_func = _evaluateGet;
        evalset_func = _evaluateSet;
      }

      //update values
      if (begin_getter != null) {
        begin_value = begin_getter();
      }

      if (end_getter != null) {
        end_value = end_getter();
      }

      UpdateDeltaValue();
      current_value = begin_value;

      // autoupdate
      switch (autoupdate_mode) {
        case TweenAutoUpdateMode.UPDATE:
          TweenRuntime.Instance.AddUpdateOnMT(Update);
          break;
        case TweenAutoUpdateMode.APPLY:
          TweenRuntime.Instance.AddUpdateOnMT(UpdateAndApply);
          break;
      }
    }

    void _doComplete(bool callEvents) {
      if (IsDelaying) {
        if (duration_getter != null) {
          Duration = duration_getter();
        }

        timer.Reset(Duration);
        IsDelaying = false;
        evalget_func = _evaluateGet;
        evalset_func = _evaluateSet;
      }
      else {
        ++LoopsCompleted;
        if (callEvents) {
          if (loop_complete_once_event != null) {
            TweenRuntime.Instance.InvokeOnMT(loop_complete_once_event);
            loop_complete_once_event = null;
          }

          if (loop_complete_event != null) {
            TweenRuntime.Instance.InvokeOnMT(loop_complete_event);
          }
        }

        if (loop_count <= 0 || LoopsCompleted < loop_count) {
          _doRestart(delay_getter?.Invoke() ?? Delay, duration_getter?.Invoke() ?? Duration);
        }
        else {
          if (dynamic_end) {
            evalget_func = _evaluateGet_EndDynamic;
            evalset_func = _evaluateSet_EndDynamic;
          }
          else {
            // autoupdate
            switch (autoupdate_mode) {
              case TweenAutoUpdateMode.UPDATE:
                TweenRuntime.Instance.RemoveUpdateOnMT(Update);
                break;
              case TweenAutoUpdateMode.APPLY:
                TweenRuntime.Instance.RemoveUpdateOnMT(UpdateAndApply);
                break;
            }

            //set current_value to end
            UpdateCurrentValue(Duration, Duration);
            setter?.Invoke(current_value);

            evalget_func = _evaluateGet_Inactive;
            evalset_func = _evaluateSet_Inactive;
          }

          if (callEvents) {
            if (complete_once_event != null) {
              TweenRuntime.Instance.InvokeOnMT(complete_once_event);
              complete_once_event = null;
            }

            if (complete_event != null) {
              TweenRuntime.Instance.InvokeOnMT(complete_event);
            }
          }
        }
      }
    }
  }

  public class FloatTween : Tween<float> {
    protected override void UpdateCurrentValue(float time, float duration) {
      var delta_needs_update = false;
      if (dynamic_begin) {
        begin_value = begin_getter();
        delta_needs_update = true;
      }

      if (dynamic_end) {
        end_value = end_getter();
        delta_needs_update = true;
      }

      if (delta_needs_update) {
        UpdateDeltaValue();
      }

      current_value = ease_function(time / duration) * delta_value + begin_value;
    }

    protected override void UpdateDeltaValue() {
      delta_value = end_value - begin_value;
    }
  }

  public class Vector3Tween : Tween<UnityEngine.Vector3> {
    protected override void UpdateCurrentValue(float time, float duration) {
      var delta_needs_update = false;
      if (dynamic_begin) {
        begin_value = begin_getter();
        delta_needs_update = true;
      }

      if (dynamic_end) {
        end_value = end_getter();
        delta_needs_update = true;
      }

      if (delta_needs_update) {
        UpdateDeltaValue();
      }

      float timeNorm = time / duration;

      current_value.x = ease_function(timeNorm) * delta_value.x + begin_value.x;
      current_value.y = ease_function(timeNorm) * delta_value.y + begin_value.y;
      current_value.z = ease_function(timeNorm) * delta_value.z + begin_value.z;
    }

    protected override void UpdateDeltaValue() {
      delta_value = end_value - begin_value;
    }
  }
}
