using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace MyRoboControls
{
    using rMind.Controls.Entities;
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            tree.SetRoot(Tree());
        }

        TreeSelectorItem Tree()
        {
            var tree = new TreeSelectorItem();

            TreeSelectorItem item;
            TreeSelectorItem subItem;

            tree.Children.Add(new TreeSelectorItem(tree) { Name = "Item 1" });
            tree.Children.Add(new TreeSelectorItem(tree) { Name = "Item 2" });

            item = new TreeSelectorItem(tree) { Name = "Item 3" };

            item.Children.Add(new TreeSelectorItem(item) { Name = "SubItem 1" });
            item.Children.Add(new TreeSelectorItem(item) { Name = "SubItem 2" });

            subItem = new TreeSelectorItem(item) { Name = "SubItem 3" };

            subItem.Children.Add(new TreeSelectorItem(subItem) { Name = "SubItem 3.1" });
            subItem.Children.Add(new TreeSelectorItem(subItem) { Name = "SubItem 3.2" });

            item.Children.Add(subItem);

            item.Children.Add(new TreeSelectorItem(item) { Name = "SubItem 4" });


            tree.Children.Add(item);
            tree.Children.Add(new TreeSelectorItem(tree) { Name = "Item 4" });

            return tree;
        }
    }
}
