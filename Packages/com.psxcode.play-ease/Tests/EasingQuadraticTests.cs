using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingQuadraticTests {
    [Test]
    public void QuadraticIn() {
      Utils.AreEqual(
        0f,
        Easing.QuadIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuadIn(1f)
      );

      Utils.AreEqual(
        0.25f,
        Easing.QuadIn(0.5f)
      );
    }

    [Test]
    public void QuadraticOut() {
      Utils.AreEqual(
        0f,
        Easing.QuadOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuadOut(1f)
      );

      Utils.AreEqual(
        0.75f,
        Easing.QuadOut(0.5f)
      );
    }

    [Test]
    public void QuadraticInOut() {
      Utils.AreEqual(
        0f,
        Easing.QuadInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuadInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.QuadInOut(0.5f)
      );
    }

    [Test]
    public void QuadraticOutIn() {
      Utils.AreEqual(
        0f,
        Easing.QuadOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuadOutIn(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.QuadOutIn(0.5f)
      );
    }
  }
}
