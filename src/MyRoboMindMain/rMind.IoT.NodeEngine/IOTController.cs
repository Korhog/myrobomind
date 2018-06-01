using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.IoT.NodeEngine
{
    using rMind.CanvasEx;
    using rMind.Elements;
    using rMind.SmartHome;

    public class IOTController : rMindBaseController
    {
        public IOTController(rMindCanvasController parent) : base(parent)
        {

        }

        /// <summary> Создаем новый контейнер </summary>
        /// <typeparam name="T">наследник от IIOTDevice</typeparam>
        public rMindBaseElement Add<T>() where T : IIOTDevice
        {
            var element = new IOTContainer<T>(this, Guid.NewGuid());
            AddElement(element);
            return element;
        }
    }
}
