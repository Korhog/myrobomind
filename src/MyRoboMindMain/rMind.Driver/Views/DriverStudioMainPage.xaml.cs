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

            var menu = TreeNodeBuilder.Build(db.SystemDrv.Drivers);
            mainMenu.ItemsSource = menu;
        }



        private void OnSelectDriver(object sender, ItemClickEventArgs e)
        {
            var item = e.ClickedItem as TreeNode;
            if (item == null)
                return;

            switch (item.Type)
            {
                case NodeType.Driver:
                    contentFrame.Navigate(typeof(DriverView), item.Driver);
                    break;
            }
        }
    }
}
