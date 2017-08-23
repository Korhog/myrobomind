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

using rMind.Elements;
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

            controller = new rMindBaseController();
            controller.Subscribe(canvas, scroll);           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var container = new rMind.Content.rMindHeaderRowContainer(controller);
            container.Static = false;
            container.BorderRadius = new CornerRadius(3);

            container.SetPosition(250, 120);
            controller.AddElement(container);
        }

        private void Button_B_Click(object sender, RoutedEventArgs e)
        {
            var container = new rMind.Content.rMindQuadContainer(controller);
            container.BorderRadius = new CornerRadius(3);
            container.SetPosition(250, 120);
            controller.AddElement(container);
        }
    }
}
