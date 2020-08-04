using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingCircularTests {
    [Test]
    public void CircularIn() {
      Utils.AreEqual(
        0f,
        Easing.CircIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.CircIn(1f)
      );

      Utils.AreEqual(
        0.133974612f,
        Easing.CircIn(0.5f)
      );
    }

    [Test]
    public void CircularOut() {
      Utils.AreEqual(
        0f,
        Easing.CircOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.CircOut(1f)
      );

      Utils.AreEqual(
        0.866025388f,
        Easing.CircOut(0.5f)
      );
    }

    [Test]
    public void CircularInOut() {
      Assert.AreEqual(
        0f,
        Easing.CircInOut(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.CircInOut(1f)
      );

      Assert.AreEqual(
        0.5f,
        Easing.CircInOut(0.5f)
      );
    }

    [Test]
    public void CircularOutIn() {
      Assert.AreEqual(
        0f,
        Easing.CircOutIn(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.CircOutIn(1f)
      );

      Assert.AreEqual(
        0.5f,
        Easing.CircOutIn(0.5f)
      );
    }
  }
}
