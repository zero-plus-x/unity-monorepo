namespace Swarm {
  public interface IServiceInstance { }

  public interface IStartServiceInstance : IServiceInstance {
    void ServiceStart();
  }

  public interface IUpdateServiceInstance : IServiceInstance {
    void ServiceUpdate();
  }
}
