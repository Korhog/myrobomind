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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace MyRoboMind
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            var controller = new rMindBaseController();
            controller.Subscribe(canvas, scroll);

            rMindBaseElement container;

            container = new rMindBaseElement(controller);
            controller.Add(container);
            container.Translate(new rMind.Types.Vector2(20, 20));

            container = new rMindBaseElement(controller);
            controller.Add(container);
            container.Translate(new rMind.Types.Vector2(20, 70));
        }



        private void Rectangle_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
