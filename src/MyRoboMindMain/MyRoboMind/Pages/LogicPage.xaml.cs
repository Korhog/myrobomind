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

namespace MyRoboMind.Pages
{
    using rMind.Elements;
    using rMind.Project;
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class LogicPage : Page
    {
        public LogicPage()
        {
            this.InitializeComponent();

            var project = rMind.Project.rMindProject.GetInstance();
            project.SetupLogic(canvas, scroll);

            BreadCrumbs.ItemsSource = project.LogicController.BreadCrumbs;

            Loaded += (sender, o) =>
            {
                project.LogicController.Draw();
            };
        }

        private void BreadCrumbClick(object sender, ItemClickEventArgs e)
        {
            var o = e.ClickedItem as rMindBaseController;
            if (o == null)
                return;

            rMind.Project.rMindProject.GetInstance().DeviceController.SetController(o);
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            var cont = rMindProject.GetInstance().LogicController.CurrentController;
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
    }
}
