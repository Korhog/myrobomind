using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace rMind.Controls
{
    using rMind.Driver.Entities;

    public delegate void OnCreateButtonDelegate(ITreeItem folder);

    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is TreeFolder)
                return DefaultTemplate;

            if (item is Driver.Driver)
                return ItemTemplate;

            return DefaultTemplate;
        }
    }


    public sealed class Tree : Control
    {
        ITreeItem root;

        ListView items;

        public event OnCreateButtonDelegate OnCreateButton;

        public Tree()
        {
            this.DefaultStyleKey = typeof(Tree);            
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            items = GetTemplateChild("PART_Items") as ListView;
            var adder = GetTemplateChild("PART_AddButton") as Button;
            adder.Click += (s, e) =>
            {
                ITreeItem folder = (DataContext as ITreeItem) ?? root;
                OnCreateButton?.Invoke(folder);
            };

            items.ItemClick += (s, e) =>
            {                
                var f = e.ClickedItem as TreeFolder;
                if (f != null)
                {
                    DataContext = f;
                    return;
                }                
            };

        }

        public void SetRoot(ITreeItem target)
        {
            root = target;
            DataContext = target;
        }
    }
}
