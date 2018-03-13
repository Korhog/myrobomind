using rMind.Driver;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace MyRoboMind
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = false;

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            var color = ColorHelper.FromArgb(255, 29, 29, 45);
            titleBar.ButtonBackgroundColor = color;
            titleBar.ButtonInactiveBackgroundColor = color;
            titleBar.BackgroundColor = color;
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonInactiveBackgroundColor = color;
        }

        private async void OnMindForge(object sender, RoutedEventArgs e)
        {
            var db = DriverDB.Current();
            await db.InitDB();

            Frame.Navigate(typeof(rMind.DriverForge.Views.DriverForgeMainPage));
        }

        private void OnMindEditor(object sender, RoutedEventArgs e)
        {

        }
    }
}
