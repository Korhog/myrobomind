using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Driver
{
    using Entities;

    [JsonObject(MemberSerialization.OptIn)]
    public class Driver : ITreeItem
    {
        [JsonProperty]
        public string IDS { get; set; }

        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public string ClassName { get; set; }

        [JsonProperty]

        public string SemanticName { get; set; }

        [JsonProperty]
        public string Desc { get; set; }

        public ITreeItem Parent { get; set; }

        ObservableCollection<Pin> pins;

        [JsonProperty]
        public ObservableCollection<Pin> Pins
        {
            get { return pins; }
            set
            {
                if (value == null)
                    return;
                pins = value;
            }
        }

        ObservableCollection<Method> methods;

        [JsonProperty]
        public ObservableCollection<Method> Methods
        {
            get { return methods; }
            set
            {
                if (value == null)
                    return;
                methods = value;
            }
        }

        public bool Folder { get { return false; } }

        public ObservableCollection<ITreeItem> Children { get { return null; } }

        public Driver()
        {
            pins = new ObservableCollection<Pin>();
            methods = new ObservableCollection<Method>();
        }

        /// <summary>
        /// Полная копия драйвера
        /// </summary>
        public Driver Instanciate()
        {
            var result = new Driver()
            {
                Name = "my driver",
                Pins = new ObservableCollection<Pin>(),
                Methods = new ObservableCollection<Method>()
            };

            foreach (var pin in Pins)
                result.Pins.Add(pin.Instanciate());

            foreach (var method in Methods)
                result.Methods.Add(method.Instanciate());

            return result;
        } 
    }
}
