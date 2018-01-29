namespace rMind.Driver.Entities
{
    public enum PinMode
    {
        /// <summary> Read </summary>
        INPUT,
        /// <summary> Write </summary>
        OUTPUT,
        /// <summary> Read? </summary>
        INPUT_PULLUP
    }

    /// <summary> Device pin for read or write data </summary>
    public class Pin
    {
        /// <summary> Pin mode: Read, Write </summary>
        public PinMode PinMode { get; set; }

        /// <summary> Pin name </summary>
        public string Name { get; set; }
    }
}
