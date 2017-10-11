using System;
using Windows.UI.Popups;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation.Metadata;
using Windows.UI.ViewManagement;

using rMind.Project;
using rMind.CanvasEx;
using rMind.Elements;
using rMind.Content.Quad;
using rMind.Elements.Debug;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace MyRoboMind
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        rMindBaseElement Create(rMindBaseController controller, double x, double y)
        {
            var container = new rMindBaseElement(controller);
            container.CreateNode();
            container.Translate(new rMind.Types.Vector2(x, y));
            return container;
        }

        public MainPage()
        {
            InitializeComponent();
            
            var project = rMind.Project.rMindProject.GetInstance();
            project.SetupDevice(canvas, scroll);

            BreadCrumbs.ItemsSource = project.DeviceController.BreadCrumbs;

            Loaded += async (s, e) =>
            {
                await project.RestoreState();
            };

            Window.Current.Activated += async (s, e) => {
                if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
                {
                    var statusBar = StatusBar.GetForCurrentView();
                    await statusBar.HideAsync();
                }
            };            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cont = rMindProject.GetInstance().DeviceController.CurrentController;
            if (cont == null)
                return;

            var rand = new Random();
            var container = cont.CreateElementByElementType(rElementType.RET_NONE);
            container.AccentColor = rMind.ColorContainer.rMindColors.ColorRandom(100, 200);

            var cnt = rand.Next(1, 5);
            for (int i = 0; i < cnt; i++)
            {
                (container as rMind.Content.rMindHeaderRowContainer)?.AddRow();
            } 
            container.SetPosition(cont.GetScreenCenter(container));
        }

        private void Button_B_Click(object sender, RoutedEventArgs e)
        {
            var cont = rMindProject.GetInstance().DeviceController.CurrentController;
            if (cont == null)
                return;

            var container = new rMind.Content.rMindQuadContainer(cont);
            container.AccentColor = Windows.UI.Colors.LightBlue;
            container.BorderRadius = new CornerRadius(3);

            rMindVerticalLine vline;
            rMindHorizontalLine hline;

            vline = container.CreateLine<rMindVerticalLine>() as rMindVerticalLine;
            vline.AddTopNode(); vline.AddBottomNode();
            vline = container.CreateLine<rMindVerticalLine>() as rMindVerticalLine;
            vline.AddTopNode(); vline.AddBottomNode();   
 
            hline = container.CreateLine<rMindHorizontalLine>() as rMindHorizontalLine;
            hline.AddLeftNode(); hline.AddRightNode();
            hline = container.CreateLine<rMindHorizontalLine>() as rMindHorizontalLine;
            hline.AddLeftNode(); hline.AddRightNode();

            cont.AddElement(container);
            container.SetPosition(cont.GetScreenCenter(container));
        }

        private void Button_Unsubscribe(object sender, RoutedEventArgs e)
        {
            rMindProject.GetInstance().DeviceController.SetController(null);
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            rMind.Project.rMindProject.GetInstance().DeviceController.Back();
        }

        private void BreadCrumbClick(object sender, ItemClickEventArgs e)
        {
            var o = e.ClickedItem as rMindBaseController;
            if (o == null)
                return;

            rMind.Project.rMindProject.GetInstance().DeviceController.SetController(o);
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            rMindProject.GetInstance().SaveState();
        }

        private async void onLoadClick(object sender, RoutedEventArgs e)
        {
            var diag = new MessageDialog("Reset project?") { Title = "Azaza" };
            
            diag.Commands.Add(new UICommand() { Label = "Yes", Id = 0 });
            diag.Commands.Add(new UICommand() { Label = "No", Id = 1 });

            var res = await diag.ShowAsync();
            if ((int)res.Id == 0)
                rMindProject.GetInstance().RestoreState();
        }

        private void onResetClick(object sender, RoutedEventArgs e)
        {
            rMindProject.GetInstance().Reset();
        }
    }
}
