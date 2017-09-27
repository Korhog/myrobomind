using System;
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
        rMindBaseController controller;
        rMindCanvasController canvas_controller;

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

            
            canvas_controller = new rMindCanvasController(canvas, scroll);
            BreadCrumbs.ItemsSource = canvas_controller.BreadCrumbs;

            controller = new rMindBaseController(canvas_controller);
            canvas_controller.SetController(controller);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rand = new Random();
            var container = new rMind.Content.rMindHeaderRowContainer(controller);
            container.AccentColor = rMind.ColorContainer.rMindColors.ColorRandom(100, 200);
            container.BorderThickness = new Thickness(1);
            var cnt = rand.Next(1, 5);
            for (int i = 0; i < cnt; i++)
            {
                container.AddRow();
            }            
            //container.Static = false;
            container.BorderRadius = new CornerRadius(3);            
            controller.AddElement(container);
            container.SetPosition(controller.GetScreenCenter(container));
        }

        private void Button_B_Click(object sender, RoutedEventArgs e)
        {
            var container = new rMind.Content.rMindQuadContainer(controller);
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

            controller.AddElement(container);
            container.SetPosition(controller.GetScreenCenter(container));
        }

        private void Button_Subscribe(object sender, RoutedEventArgs e)
        {
            canvas_controller.SetController(controller);
        }

        private void Button_Unsubscribe(object sender, RoutedEventArgs e)
        {
            canvas_controller.SetController(null);
        }

        private void Button_Back(object sender, RoutedEventArgs e)
        {
            canvas_controller.Back();
        }
    }
}
