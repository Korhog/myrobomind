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
    using rMind.Controls.Entities;
    using rMind.DriverForge.Dialogs;

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
            treeSelector.SetRoot(Tree());
            treeSelector.ItemSelect += (o) =>
            {
                var item = o as TreeSelectorItem;

                if (canvasController.CurrentController == null) return;

                var element = canvasController.CurrentController.CreateElementByElementType(rElementType.RET_NONE, null) as rMindHeaderRowContainer;
                if (element == null)
                    return;

                element.AccentColor = rMind.ColorContainer.rMindColors.ColorRandom();
                element.BorderRadius = new CornerRadius(3);
                element.Header = item?.Name ?? "Header";
                element.AddEffect();
                

                var rand = new Random();
                var rows = rand.Next(2, 5);
                for (int i = 0; i < rows; i++) {
                    element.AddRow(new rMind.Content.Row.rMindRow
                    {
                        InputNodeType = Nodes.rMindNodeConnectionType.Value,
                        OutputNodeType = Nodes.rMindNodeConnectionType.Container
                    });
                }

                element.SetPosition(new Types.Vector2(100, 100));
            };
        }

        TreeSelectorItem Tree()
        {
            var tree = new TreeSelectorItem();

            TreeSelectorItem item;
            TreeSelectorItem subItem;

            tree.Children.Add(new TreeSelectorItem(tree) { Name = "Item 1" });
            tree.Children.Add(new TreeSelectorItem(tree) { Name = "Item 2" });

            item = new TreeSelectorItem(tree) { Name = "Item 3" };

            item.Children.Add(new TreeSelectorItem(item) { Name = "SubItem 1" });
            item.Children.Add(new TreeSelectorItem(item) { Name = "SubItem 2" });

            subItem = new TreeSelectorItem(item) { Name = "SubItem 3" };

            subItem.Children.Add(new TreeSelectorItem(subItem) { Name = "SubItem 3.1" });
            subItem.Children.Add(new TreeSelectorItem(subItem) { Name = "SubItem 3.2" });

            item.Children.Add(subItem);

            item.Children.Add(new TreeSelectorItem(item) { Name = "SubItem 4" });


            tree.Children.Add(item);
            tree.Children.Add(new TreeSelectorItem(tree) { Name = "Item 4" });

            return tree;
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

        private async void OnCreateMethod(object sender, RoutedEventArgs e)
        {
            var diag = new CreateMethodDialog();
            var result = await diag.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                current.Methods.Add(new Method());
            }
        }
    }
}
