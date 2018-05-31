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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartHome.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Widgets : Page
    {
        public class WidgetDesc
        {
            public string Name { get; set; }
            public string Desc { get; set; }
        }
        
        public Widgets()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;

            WidgetGrid.ItemsSource = new WidgetDesc[] {
                new WidgetDesc { Name = "Зал" },
                new WidgetDesc { Name = "Кухня" },
                new WidgetDesc { Name = "Сад", Desc = "система полива цветов" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
                new WidgetDesc { Name = "Заглушка", Desc = "Заглушка для чего то там" },
            };

            WidgetGrid.Loaded += async (s, e) =>
            {
                if(_item != null)
                {
                    WidgetGrid.ScrollIntoView(_item, ScrollIntoViewAlignment.Default);
                    WidgetGrid.UpdateLayout();

                    var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("back");
                    if (animation != null)
                    {
                        await WidgetGrid.TryStartConnectedAnimationAsync(animation, _item, "ConnectedBorder");
                    }
                }
            };
        }

        WidgetDesc _item;

        private void OnWidgetDetails(object sender, ItemClickEventArgs e)
        {
            _item = null;
            var container = WidgetGrid.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if (container != null)
            {
                _item = container.Content as WidgetDesc;
                var animation = WidgetGrid.PrepareConnectedAnimation("call", _item, "ConnectedBorder");
            }

            Frame.Navigate(typeof(WidgetDetailsPage), _item);
        }
    }
}
