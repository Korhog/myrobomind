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
            Loaded += async (sender, e) => 
            {
                var db = DriverDB.Current();
                await db.InitDB();

                drivers.ItemsSource = db.Drivers.Drivers;
            };
        }        

        private void OnSideMenuClick(object sender, RoutedEventArgs e)
        {
            sideMenu.IsPaneOpen = !sideMenu.IsPaneOpen;
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            await DriverDB.Current().Save();
        }

        private async void OnCreate(object sender, RoutedEventArgs e)
        {
            StackPanel panel = new StackPanel();
            StackPanel idsPanel = new StackPanel { Orientation = Orientation.Horizontal };


            idsPanel.Children.Add(new TextBlock { Text = "IDS" });
            var errorText = new TextBlock {
                Text = "Error",
                Foreground = new SolidColorBrush(Colors.Red),
                Visibility = Visibility.Collapsed
            };
            idsPanel.Children.Add(errorText);

            var idsBox = new TextBox();

            panel.Children.Add(idsPanel);
            panel.Children.Add(idsBox);

            var parents = new string[] { "NONE" };
            parents = parents.Union(DriverDB.Current().Drivers.Drivers.Select(x => x.IDS)).ToArray();

            ComboBox cbParent = new ComboBox
            {
                ItemsSource = parents
            };
            panel.Children.Add(new TextBlock { Text = "Parent" });
            panel.Children.Add(cbParent);

            ContentDialog contentDialog = new ContentDialog()
            {
                PrimaryButtonText = "Create",
                SecondaryButtonText = "Cancel",
                Content = panel
            };

            contentDialog.PrimaryButtonClick += async (s, args) =>
            {                
                ContentDialogButtonClickDeferral deferral = args.GetDeferral();

                var exists = DriverDB.Current().Drivers.Drivers.Any(x => x.IDS.ToLower() == idsBox.Text.ToLower());
                if (exists)
                {
                    errorText.Visibility = Visibility.Visible;
                    args.Cancel = true;
                }         
                deferral.Complete();
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                var ids = idsBox.Text.ToUpper();
                var parentIds = (string)cbParent.SelectedItem;
                Driver parent = null;
                if (!string.IsNullOrEmpty(parentIds) && parentIds != "NONE")
                    parent = DriverDB.Current().Drivers.Drivers.Where(x => x.IDS == parentIds).FirstOrDefault();

                Driver driver = null;
                if (parent == null)
                {
                    driver = new Driver()
                    {
                        IDS = ids,
                        Name = "New Driver"
                    };
                }
                else
                {
                    driver = parent.Instanciate();
                    driver.IDS = ids;                    
                }

                var db = DriverDB.Current();
                db.Drivers.Drivers.Add(driver);
                drivers.SelectedItem = drivers.Items.LastOrDefault();
            }
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
    }
}
