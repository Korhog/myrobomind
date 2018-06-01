using System;
using System.Collections.Generic;
using System.Text;

namespace rMind.SmartHome
{
    /// <summary>
    /// Выделенное окружение IoT устройств.
    /// </summary>
    public interface IPlayground
    {
        /// <summary> Запуск </summary>
        bool Run();        
    }

    /// <summary>
    /// Выделенное окружение IoT устройств.
    /// </summary>
    public class Playground : IPlayground
    {
        /// <summary> Запуск </summary>
        public bool Run()
        {
            return true;
        }

        /// <summary> Запуск </summary>
        protected virtual bool Execute()
        {
            return true;
        }
    }
}
