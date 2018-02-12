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

namespace rMind.Driver.Views
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DriverStudioMainPage : Page
    {
        public DriverStudioMainPage()
        {
            this.InitializeComponent();
        }

        private void OnSideMenuClick(object sender, RoutedEventArgs e)
        {
            sideMenu.IsPaneOpen = !sideMenu.IsPaneOpen;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var db = rMindDataBase.GetInstance();
            await db.Load();

            foreach(var d in db.SystemDrv.Drivers)
            {
                mainTree.Items.Add(d);
            }
        }

        private void OnMainTreeClick(object sender, ItemClickEventArgs e)
        {
            var v = e;
            var s = sender;
        }
    }
}
