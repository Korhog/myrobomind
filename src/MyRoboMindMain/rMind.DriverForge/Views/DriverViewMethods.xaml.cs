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
    using rMind.Elements;
    using rMind.CanvasEx;
    using rMind.Content;

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class DriverViewMethods : Page
    {
        Driver current;
        rMindCanvasController canvasController;

        public DriverViewMethods()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            current = e.Parameter as Driver;
            methods.ItemsSource = current.Methods;

            canvasController = new rMindCanvasController(canvas, scroll);
            canvasController.Draw();
            breadCrumbs.ItemsSource = canvasController.BreadCrumbs;
        }

        private async void OnRemoveMethod(object sender, RoutedEventArgs e)
        {
            var method = (sender as Button)?.DataContext as Method;
            if (method == null)
                return;

            ContentDialog contentDialog = new ContentDialog()
            {
                Title = "Warning",
                Content = "Delete this method?",
                PrimaryButtonText = "Yes",
                SecondaryButtonText = "No"
            };

            if (await contentDialog.ShowAsync() == ContentDialogResult.Primary)
            {
                current.Methods.Remove(method);
            }
        }

        private void OnMethodChange(object sender, SelectionChangedEventArgs e)
        {
            var method = (sender as ListBox)?.SelectedItem as Method;
            if (method == null)
                return;

            canvasController.SetController(method.Controller);            
        }

        private void OnCreateElement(object sender, RoutedEventArgs e)
        {
            if (canvasController.CurrentController == null) return;

            var element = canvasController.CurrentController.CreateElementByElementType(rElementType.RET_NONE, null);
            element.AccentColor = rMind.ColorContainer.rMindColors.ColorRandom();
            (element as rMindHeaderRowContainer)?.AddRow(new rMind.Content.Row.rMindRow
            {
                InputNodeType = Nodes.rMindNodeConnectionType.Value,
                OutputNodeType = Nodes.rMindNodeConnectionType.Container
            });

            element.SetPosition(new Types.Vector2(100, 100));
        }

        private void OnBreadCrumbsClick(object sender, ItemClickEventArgs e)
        {
            var controller = e.ClickedItem as rMindBaseController;
            if (controller == null)
                return;

            canvasController.SetController(controller);            
        }

        private void OnCreateMethod(object sender, RoutedEventArgs e)
        {
            current.Methods.Add(new Method());
        }
    }
}
