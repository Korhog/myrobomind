using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace rMind.Driver.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TreeFolder : ITreeItem
    {
        [JsonProperty]
        public string Name { get; set; }

        [JsonProperty]
        public ObservableCollection<TreeFolder> Folders { get; set; }
                
        [JsonProperty]
        public ObservableCollection<Driver> Drivers { get; set; }

        public void Update()
        {
            Children.Clear();

            foreach (var folder in (Folders ?? new ObservableCollection<TreeFolder>()))
            {
                folder.Parent = this;
                folder.Update();
                Children.Add(folder);
            }

            foreach (var drv in (Drivers ?? new ObservableCollection<Driver>()))
            {
                drv.Parent = this;
                Children.Add(drv);
            }
        }

        public void AddFolder(TreeFolder folder)
        {
            if (Folders == null)
                Folders = new ObservableCollection<TreeFolder>();
            Folders.Add(folder);

            var idx = Children.IndexOf(Children.Where(x => x is TreeFolder).LastOrDefault()) + 1;
            Children.Insert(idx, folder);
        }

        public void AddDriver(Driver driver)
        {
            if (Drivers == null)
                Drivers = new ObservableCollection<Driver>();
            Drivers.Add(driver);
            Children.Add(driver);
        }

        public void Remove(ITreeItem item)
        {
            if (item is Driver)
            {
                Drivers.Remove(item as Driver);
                Children.Remove(item);
            }
            else if (item is TreeFolder)
            {
                Folders.Remove(item as TreeFolder);
                Children.Remove(item);
            }
        }

        public bool Folder { get { return true; } }

        public ITreeItem Parent { get; set; }

        public TreeFolder()
        {
            Children = new ObservableCollection<ITreeItem>(); 
        }

        public ObservableCollection<ITreeItem> Children { get; private set; }                    
    }
}
