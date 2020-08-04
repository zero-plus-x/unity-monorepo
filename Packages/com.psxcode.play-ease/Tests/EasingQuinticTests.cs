using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingQuinticTests {
    [Test]
    public void QuinticIn() {
      Utils.AreEqual(
        0f,
        Easing.QuintIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuintIn(1f)
      );

      Utils.AreEqual(
        0.03125f,
        Easing.QuintIn(0.5f)
      );
    }

    [Test]
    public void QuinticOut() {
      Utils.AreEqual(
        0f,
        Easing.QuintOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuintOut(1f)
      );

      Utils.AreEqual(
        0.96875f,
        Easing.QuintOut(0.5f)
      );
    }

    [Test]
    public void QuinticInOut() {
      Utils.AreEqual(
        0f,
        Easing.QuintInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuintInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.QuintInOut(0.5f)
      );
    }

    [Test]
    public void QuinticOutIn() {
      Utils.AreEqual(
        0f,
        Easing.QuintOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuintOutIn(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.QuintOutIn(0.5f)
      );
    }
  }
}
