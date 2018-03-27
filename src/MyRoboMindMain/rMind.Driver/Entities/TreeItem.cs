using System;
using System.Collections.ObjectModel;

namespace rMind.Driver.Entities
{
    /// <summary> Элемент дерева </summary>
    public interface ITreeItem
    {
        string Name { get; set; }

        bool Folder { get; }

        ITreeItem Parent { get; set; }

        ObservableCollection<ITreeItem> Children { get; }
    }

    public abstract class TreeFolderBase : ITreeItem
    {
        public string Name { get; set; }
        public bool Folder { get { return true; } }
        public ITreeItem Parent { get; set; }
        public abstract ObservableCollection<ITreeItem> Children { get; }
    }

    public abstract class TreeItemBase : ITreeItem
    {
        public string Name { get; set; }
        public bool Folder { get { return false; } }
        public ITreeItem Parent { get; set; }
        public ObservableCollection<ITreeItem> Children { get { return null; } }
    }
}
