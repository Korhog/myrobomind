using System;
using System.Collections.Generic;
using System.Text;
using Unosquare.RaspberryIO;
using Unosquare.RaspberryIO.Gpio;

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
        object State();
    }

    /// <summary> устройство интернета вещей </summary>
    public class IOTDevice : IIOTDevice
    {
        /// <summary> Состояние устройства </summary>
        public virtual object State()
        {
            return new {
                PIN = 5,
                Name = "LED",
                State = "Enabled"
            };
        }
    }
}
