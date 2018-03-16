using rMind.Driver;
using rMind.Driver.Entities;
using rMind.DriverForge.Dialogs;
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
    public sealed partial class DriversViewMain : Page
    {
        public DriversViewMain()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Required;

            library.SetRoot(DriverDB.Current().Drivers.Root);
            library.OnCreateButton += async (o) =>
            {
                var folder = o as TreeFolder;
                if (folder == null)
                    return;

                var diag = new CreateItemDialog();
                var result = await diag.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    if (diag.IsDriver)
                    {
                        folder.AddDriver(new Driver.Driver()
                        {
                            Name = "Name",
                            SemanticName = "Semantic Name"
                        });
                    }
                    else
                    {
                        folder.AddFolder(new TreeFolder {
                            Parent = folder,
                            Name = "Folder"
                        });
                    }
                }
            };
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            await DriverDB.Current().Save();
        }
    }
}
