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
    using System.ComponentModel;

    [JsonObject(MemberSerialization.OptIn)]
    public class Driver : ITreeItem, INotifyPropertyChanged
    {
        [JsonProperty]
        public string IDS { get; set; }

        private string m_name;
        [JsonProperty]
        public string Name
        {
            get { return m_name; }
            set {
                if (m_name == value) return;
                m_name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        [JsonProperty]
        public string ClassName { get; set; }

        private string m_semanticName;
        [JsonProperty]
        public string SemanticName
        {
            get { return m_semanticName; }
            set
            {
                if (m_semanticName == value) return;
                m_semanticName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SemanticName"));
            }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;

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
