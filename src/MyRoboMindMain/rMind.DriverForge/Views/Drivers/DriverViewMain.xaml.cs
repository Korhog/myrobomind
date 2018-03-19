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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace rMind.DriverForge.Views.Drivers
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DriverViewMain : Page
    {
        public DriverViewMain()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var driver = e.Parameter as Driver.Driver;
            if (driver == null)
                return;

            DataContext = driver;
        }

        private void SelectPage(object sender, SelectionChangedEventArgs e)
        {
            var tag = ((sender as ListBox)?.SelectedItem as ListBoxItem)?.Content?.ToString() ?? "NONE";
            if (tag == "NONE")
                return;

            if (tag == "Main")
            {
                content.Navigate(typeof(DriverViewInfo), DataContext);
            }

            if (tag == "Methods")
            {
                content.Navigate(typeof(MethodsView), DataContext);
            }
        }
    }
}
