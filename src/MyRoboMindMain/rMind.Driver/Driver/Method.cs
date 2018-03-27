using Newtonsoft.Json;
using rMind.Driver.Entities;
using rMind.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Driver
{
    /// <summary> Тип метода </summary>
    public enum MethodType
    {
        /// <summary> Чистый скрипт на языке C или Assembler </summary>
        Script,
        /// <summary> Код построенный на базе блоков </summary>
        NodeBased
    }

    /// <summary> Метод драйвера или платы </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Method : TreeItemBase
    {
        [JsonProperty]
        public string XML {
            get { return controller.Serialize().ToString(); }
            set { controller.Deserialize(value); }
        }

        /// <summary> Тип метода </summary>
        [JsonProperty]
        public MethodType MethodType { get; set; }

        /// <summary> Имя метода </summary>
        [JsonProperty]
        public string Name { get; set; }

        /// <summary> Семантическое имя метода </summary>
        [JsonProperty]
        public string SemanticName { get; set; }

        public Method()
        {
            controller = new rMindBaseController(null);
        }

        public Method Instanciate()
        {
            var result = new Method
            {
               MethodType = this.MethodType
            };

            return result;
        }

        rMindBaseController controller;
        public rMindBaseController Controller { get { return controller; } }
    }
}
