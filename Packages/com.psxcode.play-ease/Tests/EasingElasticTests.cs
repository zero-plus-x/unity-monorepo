using NUnit.Framework;

namespace PlayEase.Tests {
  public class EasingElasticTests {
    [Test]
    public void ElasticIn() {
      Utils.AreEqual(
        0f,
        Easing.ElasticIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ElasticIn(1f)
      );

      Utils.AreEqual(
        -0.0156250112,
        Easing.ElasticIn(0.5f)
      );
    }

    [Test]
    public void ElasticOut() {
      Utils.AreEqual(
        0f,
        Easing.ElasticOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ElasticOut(1f)
      );

      Utils.AreEqual(
        1.015625f,
        Easing.ElasticOut(0.5f)
      );
    }

    [Test]
    public void ElasticInOut() {
      Utils.AreEqual(
        0f,
        Easing.ElasticInOut(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ElasticInOut(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.ElasticInOut(0.5f)
      );
    }

    [Test]
    public void ElasticOutIn() {
      Utils.AreEqual(
        0f,
        Easing.ElasticOutIn(0f)
      );

      Utils.AreEqual(
        1f,
        Easing.ElasticOutIn(1f)
      );

      Utils.AreEqual(
        0.5f,
        Easing.ElasticOutIn(0.5f)
      );
    }
  }
}
