using System;
using System.Collections.Generic;
using System.Text;

namespace rMind.SmartHome
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Getter : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class Setter : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class Activator : Attribute { }

    [AttributeUsage(AttributeTargets.Method)]
    public class Receiver : Attribute { }

    [AttributeUsage(AttributeTargets.Class)]
    public class DisplayName : Attribute {
        public DisplayName(string name)
        {
            Value = name;
        }
        public string Value { get; set; }
    }

    public interface IIOTDevice
    {
    }

    /// <summary> устройство интернета вещей </summary>
    public class IOTDevice : IIOTDevice
    {
    }
}
