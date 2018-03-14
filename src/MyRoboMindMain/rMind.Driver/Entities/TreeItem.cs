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
}
