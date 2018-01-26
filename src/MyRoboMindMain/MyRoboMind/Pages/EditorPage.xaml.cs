using System;
using rMind.Driver;
using rMind.Project;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace MyRoboMind.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class EditorPage : Page
    {
        public EditorPage()
        {
            InitializeComponent();
            DeviceFrame.Navigate(typeof(DevicePage), null);
            LogicFrame.Navigate(typeof(LogicPage), null);

            Loaded += async (sender, e) => {
                await DriverController.GetInstance().Load();
                await rMindProject.GetInstance().RestoreState();
            };          


            var DB =rMindDataBase.GetInstance();
            DB.StateChanged += async (sender, e) => { 

            };
        }

        private async void Test(object sender, RoutedEventArgs e)
        {
            var controller = DriverController.GetInstance();
            await controller.Test();
        }

        private void OnDriverStudio(object sender, RoutedEventArgs e)
        {
            DriverStudioController.RunDriverStudio();
        }

        private async void Load(object sender, RoutedEventArgs e)
        {

            var controller = DriverController.GetInstance();
            await controller.Load();
        }
    }
}
