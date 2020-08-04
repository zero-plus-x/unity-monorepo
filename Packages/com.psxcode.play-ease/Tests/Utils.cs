using System;
using NUnit.Framework;

namespace PlayEase.Tests {
  public static class Utils {
    static double EPSILON = 0.001;

    public static void AreEqual(float expected, float actual) {
      if (Math.Abs(expected - actual) > EPSILON) {
        Assert.AreEqual(expected, actual);
      }
    }

    public static void AreEqual(double expected, double actual) {
      if (Math.Abs(expected - actual) > EPSILON) {
        Assert.AreEqual(expected, actual);
      }
    }
  }
}
