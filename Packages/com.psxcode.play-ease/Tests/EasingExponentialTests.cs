using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingExponentialTests {
    [Test]
    public void ExponentialIn() {
      Utils.AreEqual(
        0f,
        Easing.ExpoIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ExpoIn(1f)
      );

      Utils.AreEqual(
        0.03125f,
        Easing.ExpoIn(0.5f)
      );
    }

    [Test]
    public void ExponentialOut() {
      Utils.AreEqual(
        0f,
        Easing.ExpoOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ExpoOut(1f)
      );

      Utils.AreEqual(
        0.96875f,
        Easing.ExpoOut(0.5f)
      );
    }

    [Test]
    public void ExponentialInOut() {
      Utils.AreEqual(
        0f,
        Easing.ExpoInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ExpoInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.ExpoInOut(0.5f)
      );
    }

    [Test]
    public void ExponentialOutIn() {
      Utils.AreEqual(
        0f,
        Easing.ExpoOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ExpoOutIn(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.ExpoOutIn(0.5f)
      );
    }
  }
}
