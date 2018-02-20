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
        public string SemanticName { get; set; }

        [JsonProperty]
        public string Desc { get; set; }

        [JsonProperty]
        public ObservableCollection<Pin> Pins { get; set; }

        /// <summary>
        /// Полная копия драйвера
        /// </summary>
        public Driver Instanciate()
        {
            var result = new Driver()
            {     
                Name = "my driver",
                Pins = new ObservableCollection<Pin>()
            };

            foreach (var pin in Pins)
                result.Pins.Add(pin.Instanciate());
            
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
