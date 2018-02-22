using Newtonsoft.Json;
using rMind.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Driver
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Method
    {
        [JsonProperty]
        public string XML {
            get { return controller.Serialize().ToString(); }
            set { controller.Deserialize(value); }
        }

        public Method()
        {
            controller = new rMindBaseController(null);
        }

        public Method Instanciate()
        {
            var result = new Method
            {
               
            };

            return result;
        }

        rMindBaseController controller;
        public rMindBaseController Controller { get { return controller; } }

    }
}
