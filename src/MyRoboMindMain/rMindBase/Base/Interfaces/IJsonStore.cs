using Newtonsoft.Json;

namespace rMind.Storage
{
    /// <summary>
    /// get and set json object
    /// </summary>
    public interface IJsonStore
    {
        object Save();
        void Load(object json);
    }
}
