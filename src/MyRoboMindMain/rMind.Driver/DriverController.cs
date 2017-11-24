using System;
using System.Collections.Generic;

namespace rMind.Driver
{
    using Entities;
    using Newtonsoft.Json;
    using System.Threading.Tasks;
    using Windows.Storage;

    /// <summary> JSON база данных драйверов </summary>
    public class DriverController
    {
        private static DriverController instance;
        private static object sync = new object();

        private DriverDB driverDB;
        public DriverDB Drivers { get { return driverDB; } }

        public static DriverController GetInstance()
        {
            if (instance == null)
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new DriverController();
                    }
                }
            }

            return instance;
        }

        private DriverController()
        {

        }

        public void Test()
        {
            DriverDB db = new DriverDB();
            db.Drivers = new List<Driver>();
            db.Drivers.Add(new Driver
            {
                Name = "led",
                Description = "desc",
                SemanticName = "светодиод",
                Type = DriverType.ExternalDevice
            });

            var json = JsonConvert.SerializeObject(db);           
        }

        public async Task Load()
        {
            var documentsPath = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            var path = System.IO.Path.Combine(documentsPath, "rMind.Driver", "Content", "DB.json");

            var file = await StorageFile.GetFileFromPathAsync(path);
            var json = await FileIO.ReadTextAsync(file);

            driverDB = JsonConvert.DeserializeObject<DriverDB>(json);
        }
    }
}
