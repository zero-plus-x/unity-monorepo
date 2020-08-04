using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingLinearTests {
    [Test]
    public void Linear() {
      Utils.AreEqual(
        0f,
        Easing.Linear(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.Linear(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.Linear(0.5f)
      );
    }
  }
}
