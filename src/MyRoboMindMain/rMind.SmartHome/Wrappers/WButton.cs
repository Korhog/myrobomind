using System;
using System.Collections.Generic;
using System.Text;

namespace rMind.SmartHome
{
    [DisplayName("Button")]
    public class WButton : IOTDevice
    {
        /// <summary> Активатор пока не используется </summary>
        [Activator]
        public void Open() { }

        /// <summary> Просто проверка </summary>
        [Getter]
        public bool IsOpen() { return true; }
    }
}
