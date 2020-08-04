using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingSinusoidalTests {
    [Test]
    public void SinusoidalIn() {
      Utils.AreEqual(
        0f,
        Easing.SineIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.SineIn(1f)
      );

      Utils.AreEqual(
        0.292893231f,
        Easing.SineIn(0.5f)
      );
    }

    [Test]
    public void SinusoidalOut() {
      Utils.AreEqual(
        0f,
        Easing.SineOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.SineOut(1f)
      );

      Utils.AreEqual(
        0.707106769f,
        Easing.SineOut(0.5f)
      );
    }

    [Test]
    public void SinusoidalInOut() {
      Utils.AreEqual(
        0f,
        Easing.SineInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.SineInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.SineInOut(0.5f)
      );
    }

    [Test]
    public void SinusoidalOutIn() {
      Utils.AreEqual(
        0f,
        Easing.SineOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.SineOutIn(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.SineOutIn(0.5f)
      );
    }
  }
}
