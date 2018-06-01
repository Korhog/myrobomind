using System;
using System.Collections.Generic;
using System.Text;

namespace rMind.SmartHome
{

    /// <summary>
    /// Основной котроллер умного дома
    /// </summary>
    public class SmartHomeController
    {
        Dictionary<Guid, IIOTDevice> devices;
        public SmartHomeController()
        {
            devices = new Dictionary<Guid, IIOTDevice>();
        }

        /// <summary>
        /// Получает текущаю конфигурацию умного дома
        /// </summary>
        /// <returns></returns>
        public object GetCurrentConfig()
        {
            return null;
        }

        public bool LoadConfig(object cfg = null)
        {
            try
            {

            }
            catch
            {

            }

            return true;
        }
    }
}
