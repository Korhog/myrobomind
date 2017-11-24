namespace rMind.Driver.Entities
{
    /// <summary> Типы драйверов </summary>
    public enum DriverType
    {
        /// <summary> Библиотека (Math, ...) </summary>
        Lib,
        /// <summary> Подключаемое устройство </summary>
        ExternalDevice
    }
    /// <summary> Драйвер </summary>
    public class Driver
    {
        public string Name { get; set; }
        public string SemanticName { get; set; }
        public string Description { get; set; }
        public DriverType Type { get; set; }
    }
}
