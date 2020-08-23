using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NUnit.Framework;

namespace Swarm.RuntimeTests {
  public abstract class AServiceContainerTests {
    [Service]
    class SrvInst01 : IServiceInstance { }

    [Service]
    class SrvInst02 : IServiceInstance { }

    abstract class BaseClient01 {
      [RequireService]
      [UsedImplicitly]
      public static IServiceContainer serviceContainer { get; }

      [RequireService]
      [UsedImplicitly]
      public static SrvInst01 StaticSrv1 { get; }

      [UsedImplicitly]
      [RequireService]
      public SrvInst01 Srv01 { get; }

      protected BaseClient01() {
        serviceContainer.ResolveInstance(this);
      }
    }

    class Client01 : BaseClient01 {
      [RequireService]
      public static SrvInst02 StaticSrv2 { get; [UsedImplicitly] private set; }

      [UsedImplicitly]
      [RequireService]
      public SrvInst02 Srv02 { get; }
    }

    protected abstract IServiceContainer CreateContainer(IEnumerable<IServiceProducer> services);

    [Test]
    public void Test() {
      IServiceInstance inst01 = new SrvInst01();
      IServiceInstance inst02 = new SrvInst02();
      IServiceProducer[] services = {
        new TestServiceProducer(new[] {inst01}),
        new TestServiceProducer(new[] {inst02})
      };

      var cont = CreateContainer(services);

      cont.ResolveStatic(typeof(Client01));

      Assert.AreEqual(
        Client01.StaticSrv1,
        inst01,
        "Should resolve static"
      );

      Assert.AreEqual(
        Client01.StaticSrv2,
        inst02,
        "Should resolve static"
      );

      var client = new Client01();

      Assert.AreEqual(
        client.Srv01,
        inst01,
        "Should resolve"
      );

      Assert.AreEqual(
        client.Srv02,
        inst02,
        "Should resolve"
      );
    }
  }

  public class ServiceContainerTest : AServiceContainerTests {
    protected override IServiceContainer CreateContainer(IEnumerable<IServiceProducer> services) =>
      SimpleServiceContainer.Create(services);
  }
}
