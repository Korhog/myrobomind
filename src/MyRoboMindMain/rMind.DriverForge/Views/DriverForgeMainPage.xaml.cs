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

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var db = DriverDB.Current();
            await db.InitDB();

            drivers.ItemsSource = db.Drivers.Drivers;
            if (db.Drivers.Drivers.Count > 0)
                drivers.SelectedIndex = 0;
        }


        private void OnSideMenuClick(object sender, RoutedEventArgs e)
        {
            sideMenu.IsPaneOpen = !sideMenu.IsPaneOpen;
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            await DriverDB.Current().Save();
        }

        private void OnDriverChange(object sender, SelectionChangedEventArgs e)
        {
            var driver = (sender as ListBox)?.SelectedItem as Driver;
            if (driver == null)
                return;

            content.Navigate(typeof(DriverView), driver);
        }

        private async void OnDelete(object sender, RoutedEventArgs e)
        {
            var driver = (sender as Button)?.DataContext as Driver;
            if (driver == null)
                return;

            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Warning",
                Content = "Delete this driver?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"                
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                var db = DriverDB.Current();
                db.Drivers.Drivers.Remove(driver);
            }
        }

        private async void OnCreateDriver(object sender, RoutedEventArgs e)
        { 
            CreateLibItemDialog contentDialog = new CreateLibItemDialog()
            {
                PrimaryButtonText = "Create",
                SecondaryButtonText = "Cancel"
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {

                if (contentDialog.IsFolder)
                    return;

                var ids = contentDialog.IDS;
                var parentIds = contentDialog.ParentTemplate;
                Driver parent = null;
                if (!string.IsNullOrEmpty(parentIds) && parentIds != "NONE")
                    parent = DriverDB.Current().Drivers.Drivers.Where(x => x.IDS == parentIds).FirstOrDefault();

                Driver driver = null;
                if (parent == null)
                {
                    driver = new Driver()
                    {
                        IDS = ids,
                        Name = contentDialog.ElementName
                    };
                }
                else
                {
                    driver = parent.Instanciate();
                    driver.IDS = ids;
                    driver.Name = contentDialog.ElementName;
                }

                var db = DriverDB.Current();
                db.Drivers.Drivers.Add(driver);
                drivers.SelectedItem = drivers.Items.LastOrDefault();
            }
        }

        private void OnCreateFolder(object sender, RoutedEventArgs e)
        {

        }
    }
}
