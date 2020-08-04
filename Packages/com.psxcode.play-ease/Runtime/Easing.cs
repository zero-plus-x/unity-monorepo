using System;

namespace PlayEase {
  public static class Easing {
    /// <summary>
    /// Enumeration of all easing equations.
    /// </summary>
    public enum Ease {
      Linear,
      QuadOut,
      QuadIn,
      QuadInOut,
      QuadOutIn,
      ExpoOut,
      ExpoIn,
      ExpoInOut,
      ExpoOutIn,
      CubicOut,
      CubicIn,
      CubicInOut,
      CubicOutIn,
      QuartOut,
      QuartIn,
      QuartInOut,
      QuartOutIn,
      QuintOut,
      QuintIn,
      QuintInOut,
      QuintOutIn,
      CircOut,
      CircIn,
      CircInOut,
      CircOutIn,
      SineOut,
      SineIn,
      SineInOut,
      SineOutIn,
      ElasticOut,
      ElasticIn,
      ElasticInOut,
      ElasticOutIn,
      BounceOut,
      BounceIn,
      BounceInOut,
      BounceOutIn,
      BackOut,
      BackIn,
      BackInOut,
      BackOutIn
    }

    #region Linear

    /// <summary>
    /// Easing equation function for a simple linear tweening, with no easing.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float Linear(float t) {
      return t;
    }

    #endregion

    #region Expo

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoOut(float t) {
      return t > 0.999f ? 1f : (float) -Math.Pow(2.0, -10.0 * t) + 1f;
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoIn(float t) {
      return t == 0 ? 0 : (float) Math.Pow(2.0, 10f * (t - 1f));
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoInOut(float t) {
      if (t == 0)
        return 0;

      if (t > 0.999f)
        return 1f;

      if ((t *= 2f) < 1)
        return 0.5f * (float) Math.Pow(2.0, 10f * (t - 1f));

      return 0.5f * ((float) -Math.Pow(2.0, -10f * (t - 1f)) + 2f);
    }

    /// <summary>
    /// Easing equation function for an exponential (2^t) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ExpoOutIn(float t) {
      if (t < 0.5f)
        return ExpoOut(t * 2f) * .5f;

      return ExpoIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Circular

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CircOut(float t) {
      return (float) Math.Sqrt(1f - (t -= 1f) * t);
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CircIn(float t) {
      return -1f * ((float) Math.Sqrt(1f - t * t) - 1f);
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CircInOut(float t) {
      if ((t *= 2f) < 1f)
        return -0.5f * ((float) Math.Sqrt(1f - t * t) - 1f);

      return 0.5f * ((float) Math.Sqrt(1f - (t -= 2f) * t) + 1f);
    }

    /// <summary>
    /// Easing equation function for a circular (sqrt(1-t^2)) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CircOutIn(float t) {
      if (t < 0.5f)
        return CircOut(t * 2f) * .5f;

      return CircIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Quad

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuadOut(float t) {
      return -t * (t - 2f);
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuadIn(float t) {
      return t * t;
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuadInOut(float t) {
      if ((t *= 2f) < 1f)
        return 0.5f * t * t;

      return -0.5f * ((--t) * (t - 2f) - 1f);
    }

    /// <summary>
    /// Easing equation function for a quadratic (t^2) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuadOutIn(float t) {
      if (t < 0.5f)
        return QuadOut(t * 2f) * .5f;

      return QuadIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Sine

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float SineOut(float t) {
      return (float) Math.Sin(t * (Math.PI / 2.0));
    }

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float SineIn(float t) {
      return (float) -Math.Cos(t * (Math.PI / 2.0)) + 1f;
    }

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float SineInOut(float t) {
      if ((t *= 2f) < 1f)
        return 0.5f * (float) Math.Sin(Math.PI * (t * 0.5f));

      return -0.5f * ((float) Math.Cos(Math.PI * (--t * 0.5f)) - 2f);
    }

    /// <summary>
    /// Easing equation function for a sinusoidal (sin(t)) easing in/out:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float SineOutIn(float t) {
      if (t < 0.5f)
        return SineOut(t * 2f) * .5f;

      return SineIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Cubic

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CubicOut(float t) {
      return (t -= 1f) * t * t + 1f;
    }

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CubicIn(float t) {
      return t * t * t;
    }

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CubicInOut(float t) {
      if ((t *= 2f) < 1f)
        return 0.5f * t * t * t;

      return 0.5f * ((t -= 2f) * t * t + 2f);
    }

    /// <summary>
    /// Easing equation function for a cubic (t^3) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float CubicOutIn(float t) {
      if (t < 0.5f)
        return CubicOut(t * 2f) * .5f;

      return CubicIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Quartic

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuartOut(float t) {
      return -((t -= 1f) * t * t * t - 1f);
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuartIn(float t) {
      return t * t * t * t;
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuartInOut(float t) {
      if ((t *= 2f) < 1f)
        return 0.5f * t * t * t * t;

      return -0.5f * ((t -= 2f) * t * t * t - 2f);
    }

    /// <summary>
    /// Easing equation function for a quartic (t^4) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuartOutIn(float t) {
      if (t < 0.5f)
        return QuartOut(t * 2f) * .5f;
      return QuartIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Quintic

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuintOut(float t) {
      return (t -= 1f) * t * t * t * t + 1f;
    }

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuintIn(float t) {
      return t * t * t * t * t;
    }

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuintInOut(float t) {
      if ((t *= 2f) < 1f)
        return 0.5f * t * t * t * t * t;
      return 0.5f * ((t -= 2f) * t * t * t * t + 2f);
    }

    /// <summary>
    /// Easing equation function for a quintic (t^5) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float QuintOutIn(float t) {
      if (t < 0.5f)
        return QuintOut(t * 2f) * .5f;
      return QuintIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Elastic

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ElasticOut(float t) {
      if (t > 0.999f)
        return 1f;

      float p = .3f;
      float s = p * 0.25f;

      return (float) Math.Pow(2.0, -10f * t) * (float) Math.Sin((t - s) * (2.0 * Math.PI) / p) + 1f;
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ElasticIn(float t) {
      if (t > 0.999f)
        return 1f;

      float p = .3f;
      float s = p * 0.25f;

      return -((float) Math.Pow(2.0, 10f * (t -= 1f)) * (float) Math.Sin((t - s) * (2.0 * Math.PI) / p));
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ElasticInOut(float t) {
      if ((t *= 2) > 1.999f)
        return 1f;

      float p = .3f * 1.5f;
      float s = p * 0.25f;

      if (t < 1f)
        return -.5f * ((float) Math.Pow(2.0, 10f * (t -= 1f)) *
                       (float) Math.Sin((t - s) * (2.0 * Math.PI) / p));
      return (float) Math.Pow(2.0, -10f * (t -= 1f)) * (float) Math.Sin((t - s) * (2.0 * Math.PI) / p) * .5f + 1f;
    }

    /// <summary>
    /// Easing equation function for an elastic (exponentially decaying sine wave) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float ElasticOutIn(float t) {
      if (t < 0.5f)
        return ElasticOut(t * 2f) * .5f;
      return ElasticIn((t * 2f) - 1f) * .5f + .5f;
    }

    #endregion

    #region Bounce

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BounceOut(float t) {
      if (t < 1f / 2.75f)
        return 7.5625f * t * t;
      else if (t < 2f / 2.75f)
        return 7.5625f * (t -= (1.5f / 2.75f)) * t + .75f;
      else if (t < 2.5f / 2.75f)
        return 7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f;
      else
        return 7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f;
    }

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BounceIn(float t) {
      return 1f - BounceOut(1f - t);
    }

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BounceInOut(float t) {
      if (t < 0.5f)
        return BounceIn(t * 2f) * .5f;
      else
        return BounceOut(t * 2f - 1f) * .5f + .5f;
    }

    /// <summary>
    /// Easing equation function for a bounce (exponentially decaying parabolic bounce) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BounceOutIn(float t) {
      if (t < 0.5f)
        return BounceOut(t * 2f) * .5f;
      return BounceIn(t * 2f - 1f) * .5f + .5f;
    }

    #endregion

    #region Back

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out:
    /// decelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BackOut(float t) {
      return (t -= 1f) * t * ((1.70158f + 1f) * t + 1.70158f) + 1f;
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in:
    /// accelerating from zero velocity.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BackIn(float t) {
      return t * t * ((1.70158f + 1f) * t - 1.70158f);
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing in/out:
    /// acceleration until halfway, then deceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BackInOut(float t) {
      var s = 1.70158f;
      if ((t *= 2) < 1)
        return .5f * (t * t * (((s *= 1.525f) + 1f) * t - s));
      return .5f * ((t -= 2f) * t * (((s *= 1.525f) + 1f) * t + s) + 2);
    }

    /// <summary>
    /// Easing equation function for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) easing out/in:
    /// deceleration until halfway, then acceleration.
    /// </summary>
    /// <param name="t">Current time in seconds.</param>
    /// <returns>The correct value.</returns>
    public static float BackOutIn(float t) {
      if (t < 0.5f)
        return BackOut(t * 2) * .5f;
      return BackIn((t * 2f) - 1) * .5f + .5f;
    }

    #endregion
  }
}
