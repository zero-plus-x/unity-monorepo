using System;

namespace Swarm {
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
  public class ServiceAttribute : Attribute {
    public ServiceAttribute() { }

    public ServiceAttribute(Type type) {
      Type = type;
    }

    public Type Type { get; set; }
  }

  [AttributeUsage(AttributeTargets.Class)]
  public class EntityAttribute : Attribute {
    public EntityAttribute() { }

    public EntityAttribute(Type type) {
      Type = type;
    }

    public Type Type { get; set; }
  }

  [AttributeUsage(AttributeTargets.Property)]
  public class RequireServiceAttribute : Attribute {
    public string Name { get; set; }
  }
}
