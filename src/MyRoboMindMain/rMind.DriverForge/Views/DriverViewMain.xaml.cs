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

namespace rMind.DriverForge.Views
{
    using rMind.Driver;
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DriverViewMain : Page
    {
        Driver current;

        public DriverViewMain()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            current = e.Parameter as Driver;
            DataContext = current;
        }

        private void OnCreatePIN(object sender, RoutedEventArgs e)
        {
            current.Pins.Add(new Pin { PinMode = PinMode.INPUT });
        }

        private async void OnRemovePIN(object sender, RoutedEventArgs e)
        {
            var pin = (sender as Button)?.DataContext as Pin;
            if (pin == null)
                return;

            var res = Resources.ThemeDictionaries.Keys.ToArray();

            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Warning",
                Content = "Delete this PIN?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                var db = DriverDB.Current();
                current.Pins.Remove(pin);
            }
        }
    }
}
