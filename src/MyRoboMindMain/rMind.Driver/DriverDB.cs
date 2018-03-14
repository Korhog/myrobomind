using Newtonsoft.Json;
using rMind.Driver.Entities;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage;

namespace rMind.Driver
{
    /// <summary> База данных </summary>
    [JsonObject(MemberSerialization.OptIn)]    
    public class DriverContext
    {
        /// <summary> Старая версия драйверов </summary>
        [JsonProperty]
        public ObservableCollection<Driver> Drivers { get; set; }

        /// <summary> Дерево драйверов </summary>
        [JsonProperty]
        public TreeFolder Root { get; set; }
    } 
    
    public class DriverDB
    {
        private DriverContext context = null;
        public DriverContext Drivers { get { return context; } }

        private static DriverDB instance;
        private static readonly object sync = new object();

        public static DriverDB Current()
        {
            lock(sync)
            {
                if(instance == null)
                {
                    instance = new DriverDB();
                }
                return instance;
            }
        }

        public async Task InitDB(bool editor = false)
        {
            StorageFile file;
            StorageFolder local = ApplicationData.Current.LocalFolder;

            try
            {
                file = await local.GetFileAsync("DriverDB.json");
            }
            catch
            {
                var documentsPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                var path = System.IO.Path.Combine(documentsPath, "rMind.Driver", "DriverDB.json");
                file = await StorageFile.GetFileFromPathAsync(path);
            }

            var json = await FileIO.ReadTextAsync(file);
            context = JsonConvert.DeserializeObject<DriverContext>(json);
            if (context.Root == null)
            {
                context.Root = new TreeFolder();
            }

            context.Root.Update();
        } 

        public async Task Save()
        {
            StorageFile file;
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var json = JsonConvert.SerializeObject(context);

            file = await local.CreateFileAsync("DriverDB.json", CreationCollisionOption.OpenIfExists);
            await FileIO.WriteTextAsync(file, json);
        }
    }
}
