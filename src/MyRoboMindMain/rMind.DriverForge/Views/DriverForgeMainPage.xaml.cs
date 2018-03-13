using System;
using System.Collections.ObjectModel;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace rMind.DriverForge.Views
{
    using Driver;
    using Dialogs;
    using System.Threading.Tasks;
    using Windows.UI;

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DriverForgeMainPage : Page
    {
        public DriverForgeMainPage()
        {
            this.InitializeComponent();
        }

        private void OnSideMenuClick(object sender, RoutedEventArgs e)
        {
            sideMenu.IsPaneOpen = !sideMenu.IsPaneOpen;
        }        

        private void OnSelectPage(object sender, SelectionChangedEventArgs e)
        {
            if (content == null)
                return;

            var tag = ((sender as ListBox)?.SelectedItem as ListBoxItem)?.Tag?.ToString() ?? "NONE";
            if (tag == "NONE")
                return;

            if (tag == "Drivers")
            {
                content.Navigate(typeof(DriversView));
            }

            if (tag == "Boards")
            {
                content.Navigate(typeof(BoardViewMain));
            }
        }
    }
}
