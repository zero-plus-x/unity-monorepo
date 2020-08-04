using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingBounceTests {
    [Test]
    public void BounceIn() {
      Utils.AreEqual(
        0f,
        Easing.BounceIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.BounceIn(1f)
      );

      Utils.AreEqual(
        0.234375f,
        Easing.BounceIn(0.5f)
      );
    }

    [Test]
    public void BounceOut() {
      Utils.AreEqual(
        0f,
        Easing.BounceOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.BounceOut(1f)
      );

      Utils.AreEqual(
        0.765625f,
        Easing.BounceOut(0.5f)
      );
    }

    [Test]
    public void BounceInOut() {
      Utils.AreEqual(
        0f,
        Easing.BounceInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.BounceInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.BounceInOut(0.5f)
      );
    }

    [Test]
    public void BounceOutIn() {
      Utils.AreEqual(
        0f,
        Easing.BounceOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.BounceOutIn(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.BounceOutIn(0.5f)
      );
    }
  }
}
