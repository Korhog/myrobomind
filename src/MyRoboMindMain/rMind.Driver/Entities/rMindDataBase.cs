using Newtonsoft.Json;
using rMind.Driver.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace rMind.Driver
{
    /// <summary> Arguments of data base state changes </summary>
    public class DBStateChangedHandlerArgs
    {

    }

    public delegate void DBStateChangedHandler(rMindDataBase sender, DBStateChangedHandlerArgs args);

    public class rMindDataBase
    {
        public event DBStateChangedHandler StateChanged;

        private static rMindDataBase instance;
        private static object sync = new object();

        private DriverDB systemDrvDB;
        public DriverDB SystemDrv { get { return systemDrvDB; } }

        rMindDataBase()
        {

        }

        public static rMindDataBase GetInstance()
        {
            if (instance == null)
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new rMindDataBase();
                    }
                }
            }
            return instance;
        }

        public async Task Load()
        {
            StorageFile file;
            StorageFolder local = ApplicationData.Current.LocalFolder;
            try
            {
                file = await local.GetFileAsync("DB.json");
            }
            catch
            {
                var documentsPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                var path = System.IO.Path.Combine(documentsPath, "rMind.Driver", "Content", "DB.json");
                file = await StorageFile.GetFileFromPathAsync(path);
            }

            var json = await FileIO.ReadTextAsync(file);
            systemDrvDB = JsonConvert.DeserializeObject<DriverDB>(json);
        }

        public async Task Save(string json)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var tmpFile = await local.CreateFileAsync("DB.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tmpFile, json);
        }
    }
}
