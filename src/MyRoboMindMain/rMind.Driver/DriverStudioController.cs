using System;

namespace rMind.Driver
{
    using Windows.UI.Core;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using rMind.Driver.Views;

    public class DriverStudioController
    {
        private DriverController driverController;

        public static async void RunDriverStudio()
        {
            CoreApplicationView studioView = CoreApplication.CreateNewView();
            int studioViewId = 0;

            await studioView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DriverController.GetInstance();

                Frame frame = new Frame();
                Window wnd = Window.Current;
                wnd.Content = frame;
                frame.Navigate(typeof(DriverStudioMainPage));
                
                wnd.Activate();

                studioViewId = ApplicationView.GetForCurrentView().Id;
            });


            bool viewShown = await ApplicationViewSwitcher.TryShowAsStandaloneAsync(studioViewId);
        }
    }
}
