using System;
using System.Linq;
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

        public async Task Test()
        {
            string name = "DC";
            if (driverDB.Drivers.Any(x => x.Name == name))
                return;

            var drv = new Driver
            {
                Name = name,
                Type = DriverType.ExternalDevice
            };

            drv.Pins.Add(new Pin { Name = "IN1", PinMode = PinMode.OUTPUT });
            drv.Pins.Add(new Pin { Name = "IN2", PinMode = PinMode.OUTPUT });
            drv.Pins.Add(new Pin { Name = "PWM", PinMode = PinMode.OUTPUT });

            driverDB.Drivers.Add(drv);

            var json = JsonConvert.SerializeObject(driverDB);
            await Save(json);
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
            driverDB = JsonConvert.DeserializeObject<DriverDB>(json);
        }

        public async Task Save(string json)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var tmpFile = await local.CreateFileAsync("DB.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tmpFile, json);            
        }
    }
}
