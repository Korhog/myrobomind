using System;
using System.Collections.Generic;
using System.Text;

namespace rMind.SmartHome
{
    [DisplayName("LED")]
    public class WLed : IOTDevice
    {
        /// <summary> Просто проверка </summary>
        [Receiver]
        public void Update() { }
        /// <summary> Просто проверка </summary>
        [Getter]
        [Setter]
        public bool Enabled() { return true; }

        [Setter]
        [Getter]
        public float Value() { return 1.0f; }
    }
}
