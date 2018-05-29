using rMind.CanvasEx;
using rMind.Content;
using rMind.Elements;
using rMind.Nodes;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace SmartHome
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        rMindCanvasController controller;

        public MainPage()
        {
            this.InitializeComponent();

            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            var color = Colors.Transparent;
            titleBar.ButtonBackgroundColor = color;
            titleBar.ButtonInactiveBackgroundColor = color;
            titleBar.BackgroundColor = color;
            titleBar.ForegroundColor = Colors.White;
            titleBar.ButtonInactiveBackgroundColor = color;

            InitCanvas();
        }

        void InitCanvas()
        {
            controller = new rMindCanvasController(canvas, scroll);
            var main = new rMindBaseController(controller);
            controller.SetController(main);
            controller.Draw();
            var rand = new Random();
            main.CreateElementByElementType(rElementType.RET_DEVICE_OUTPUT, null).Translate(new rMind.Types.Vector2(200, 200));
            for (int i = 0; i < 4; i++)
            {
                var element = main.CreateElementByElementType(rElementType.RET_NONE, null);
                element.Translate(new rMind.Types.Vector2(200, 200));
                element.AccentColor = Colors.LightSteelBlue;
                element.BorderRadius = new CornerRadius(3);

                var headerRowContainer = element as rMindHeaderRowContainer;
                if (headerRowContainer == null)
                    continue;

                headerRowContainer.Header = "Node";
                headerRowContainer.CanExpand = true;
                headerRowContainer.CanEdit = false;
                headerRowContainer.AddRowTemplate = new rMind.Content.Row.rMindRow
                {
                    InputNode = new rMindBaseNode(headerRowContainer)
                    {
                        ConnectionType = rMindNodeConnectionType.Value,
                        NodeOrientation = rMindNodeOriantation.Left,
                        AttachMode = rMindNodeAttachMode.Single,
                        Theme = new rMindNodeTheme
                        {
                            BaseStroke = new SolidColorBrush(Colors.SkyBlue),
                            BaseFill = new SolidColorBrush(Colors.Black),
                        }
                    },
                    OutputNode = new rMindBaseNode(headerRowContainer)
                    {
                        ConnectionType = rMindNodeConnectionType.Value,
                        NodeOrientation = rMindNodeOriantation.Right,
                        AttachMode = rMindNodeAttachMode.Single,
                        Theme = new rMindNodeTheme
                        {
                            BaseStroke = new SolidColorBrush(Colors.BlueViolet),
                            BaseFill = new SolidColorBrush(Colors.Black),
                        }
                    },
                };

                for (var r = 0; r < rand.Next(1, 4); r++)
                {
                    headerRowContainer.AddRow();
                }
            }

            var item = new MenuFlyoutItem
            {
                Text = "Добавить",
            };
            item.Click += (s, e) =>
            {
                var element = main.CreateElementByElementType(rElementType.RET_NONE, null);
                element.Translate(new rMind.Types.Vector2(200, 200));
                element.AccentColor = Colors.LightSteelBlue;
                element.BorderRadius = new CornerRadius(3);

                var headerRowContainer = element as rMindHeaderRowContainer;
                if (headerRowContainer == null)
                    return;

                headerRowContainer.Header = "Node";
                headerRowContainer.CanExpand = true;
                headerRowContainer.CanEdit = false;
                headerRowContainer.AddRowTemplate = new rMind.Content.Row.rMindRow
                {
                    InputNode = new rMindBaseNode(headerRowContainer)
                    {
                        ConnectionType = rMindNodeConnectionType.Value,
                        NodeOrientation = rMindNodeOriantation.Left,
                        AttachMode = rMindNodeAttachMode.Single,
                        Theme = new rMindNodeTheme
                        {
                            BaseStroke = new SolidColorBrush(Colors.SkyBlue),
                            BaseFill = new SolidColorBrush(Colors.Black),
                        }
                    },
                    OutputNode = new rMindBaseNode(headerRowContainer)
                    {
                        ConnectionType = rMindNodeConnectionType.Value,
                        NodeOrientation = rMindNodeOriantation.Right,
                        AttachMode = rMindNodeAttachMode.Single,
                        Theme = new rMindNodeTheme
                        {
                            BaseStroke = new SolidColorBrush(Colors.BlueViolet),
                            BaseFill = new SolidColorBrush(Colors.Black),
                        }
                    },
                };

                for (var r = 0; r < rand.Next(1, 4); r++)
                {
                    headerRowContainer.AddRow();
                };
            };

            main.Flyout.Items.Add(item);
        }
    }
}
