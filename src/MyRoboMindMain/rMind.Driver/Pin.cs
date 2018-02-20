using Newtonsoft.Json;

namespace rMind.Driver
{
    public enum PinMode
    {
        INPUT,
        OUTPUT,
        INPUT_PULLUP
    }

    /// <summary>
    /// Пин устройства
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Pin
    {
        [JsonProperty]
        public PinMode PinMode { get; set; } = PinMode.OUTPUT;

        public Pin Instanciate()
        {
            var result = new Pin
            {
                PinMode = PinMode
            };

            return result;
        }
    }
}
