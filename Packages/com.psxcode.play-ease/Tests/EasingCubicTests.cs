using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingCubicTests {
    [Test]
    public void CubicIn() {
      Utils.AreEqual(
        0f,
        Easing.CubicIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.CubicIn(1f)
      );

      Utils.AreEqual(
        0.125f,
        Easing.CubicIn(0.5f)
      );
    }

    [Test]
    public void CubicOut() {
      Utils.AreEqual(
        0f,
        Easing.CubicOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.CubicOut(1f)
      );

      Assert.AreEqual(
        0.875f,
        Easing.CubicOut(0.5f)
      );
    }

    [Test]
    public void CubicInOut() {
      Assert.AreEqual(
        0f,
        Easing.CubicInOut(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.CubicInOut(1f)
      );

      Assert.AreEqual(
        0.5f,
        Easing.CubicInOut(0.5f)
      );
    }

    [Test]
    public void CubicOutIn() {
      Assert.AreEqual(
        0f,
        Easing.CubicOutIn(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.CubicOutIn(1f)
      );

      Assert.AreEqual(
        0.5f,
        Easing.CubicOutIn(0.5f)
      );
    }
  }
}
