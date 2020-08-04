using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingQuarticTests {
    [Test]
    public void QuarticIn() {
      Utils.AreEqual(
        0f,
        Easing.QuartIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuartIn(1f)
      );

      Utils.AreEqual(
        0.0625f,
        Easing.QuartIn(0.5f)
      );
    }

    [Test]
    public void QuarticOut() {
      Utils.AreEqual(
        0f,
        Easing.QuartOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuartOut(1f)
      );

      Utils.AreEqual(
        0.9375f,
        Easing.QuartOut(0.5f)
      );
    }

    [Test]
    public void QuarticInOut() {
      Utils.AreEqual(
        0f,
        Easing.QuartInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuartInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.QuartInOut(0.5f)
      );
    }

    [Test]
    public void QuarticOutIn() {
      Utils.AreEqual(
        0f,
        Easing.QuartOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.QuartOutIn(1f)
      );

      Assert.AreEqual(
        0.5f,
        Easing.QuartOutIn(0.5f)
      );
    }
  }
}
