using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingBackTests {
    [Test]
    public void BackIn() {
      Utils.AreEqual(
        0f,
        Easing.BackIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.BackIn(1f)
      );

      Assert.AreEqual(
        -0.087697506f,
        Easing.BackIn(0.5f)
      );
    }

    [Test]
    public void BackOut() {
      Assert.AreEqual(
        0f,
        Easing.BackOut(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.BackOut(1f)
      );

      Assert.AreEqual(
        1.08769751f,
        Easing.BackOut(0.5f)
      );
    }

    [Test]
    public void BackInOut() {
      Assert.AreEqual(
        0f,
        Easing.BackInOut(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.BackInOut(1f)
      );

      Assert.AreEqual(
        0.49999994f,
        Easing.BackInOut(0.5f)
      );
    }

    [Test]
    public void BackOutIn() {
      Assert.AreEqual(
        0f,
        Easing.BackOutIn(0f)
      );

      Assert.AreEqual(
        1f,
        Easing.BackOutIn(1f)
      );

      Assert.AreEqual(
        0.5f,
        Easing.BackOutIn(0.5f)
      );
    }
  }
}
