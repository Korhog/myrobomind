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

namespace rMind.Editor.Views
{
    using CanvasEx;
    using Project;
    using Content;
    using Elements;
    using Windows.UI.Popups;
    using Windows.UI;

    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MindEditorMindPage : Page
    {
        CanvasEx.rMindCanvasController controller;

        public MindEditorMindPage()
        {
            this.InitializeComponent();
            controller = new rMindCanvasController(canvas, scroll);
            var main = new rMindBaseController(controller);
            controller.SetController(main);
            controller.Draw();

            main.CreateElementByElementType(rElementType.RET_DEVICE_OUTPUT, null).Translate(new Types.Vector2(200, 200));
            for (int i = 0; i < 4; i++)
            {
                var element = main.CreateElementByElementType(rElementType.RET_NONE, null);
                element.Translate(new Types.Vector2(200, 200));
                element.AccentColor = Colors.LightSteelBlue;

                (element as rMindHeaderRowContainer)?.AddRow(new rMind.Content.Row.rMindRow {
                    InputNodeType = Nodes.rMindNodeConnectionType.Value,
                    OutputNodeType = Nodes.rMindNodeConnectionType.Value,
                });

            }
        }

        private void OnSideMenuClick(object sender, RoutedEventArgs e)
        {
            sideMenu.IsPaneOpen = !sideMenu.IsPaneOpen;
        }
    }
}
