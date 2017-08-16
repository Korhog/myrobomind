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

            var controller = new rMindBaseController();
            controller.Subscribe(canvas, scroll);

            controller.AddElement(Create(controller, 10, 10));
            controller.AddElement(Create(controller, 210, 210));

            controller.AddElement(new rMindDebugContainer(controller));

            var container = new rMind.Content.rMindRowContainer(controller);
            for (int i = 0; i < 5; i++)
            {
                container.AddRow();
            }

            container.SetPosition(250, 120);
            controller.AddElement(container);
        }
    }
}
