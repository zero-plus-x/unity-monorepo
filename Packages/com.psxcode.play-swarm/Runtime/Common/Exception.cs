using System;

namespace Swarm.Common {
  public class ServiceAlreadyRegisteredException : Exception {
    public ServiceAlreadyRegisteredException(string message) : base(message) { }

    public ServiceAlreadyRegisteredException(string message, Exception innerException) :
      base(message, innerException) { }
  }

  public class ServiceNotFoundException : Exception {
    public ServiceNotFoundException(string message) : base(message) { }
    public ServiceNotFoundException(string message, Exception innerException) : base(message, innerException) { }
  }
}
