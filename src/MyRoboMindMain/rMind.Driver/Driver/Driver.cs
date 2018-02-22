using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Driver
{
    public interface ITreeItem {}

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

    [JsonObject(MemberSerialization.OptIn)]
    public class DriverFolder : ITreeItem
    {
        [JsonProperty]
        public string Caption { get; set; }

        public ObservableCollection<ITreeItem> Children { get; set; }

        public string ICO { get; set; } = "\uE188";
    }
}
