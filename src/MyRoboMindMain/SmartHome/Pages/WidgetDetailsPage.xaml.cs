using rMind.CanvasEx;
using rMind.Content;
using rMind.Elements;
using rMind.Nodes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Hosting;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using static SmartHome.Pages.Widgets;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartHome.Pages
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class WidgetDetailsPage : Page
    {
        rMindCanvasController controller;

        public WidgetDetailsPage()
        {
            this.InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            WidgetDesc desc = e.Parameter as WidgetDesc;
            DataContext = desc;

            ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("call");
            if (animation != null)
            {
                CreateAnimations();
                animation.Completed += (s, args) => {
                    grid.Visibility = Visibility.Visible;
                    coordStack.Visibility = Visibility.Visible;
                };
                animation.TryStart(Test);
            }

            InitCanvas();
        }

        void CreateAnimations()
        {
            var comp = ElementCompositionPreview.GetElementVisual(this).Compositor;

            grid.Visibility = Visibility.Collapsed;
            coordStack.Visibility = Visibility.Collapsed;

            var fadeGrid = comp.CreateScalarKeyFrameAnimation();
            fadeGrid.InsertKeyFrame(0, 0f);
            fadeGrid.InsertKeyFrame(1, 1f);
            fadeGrid.Duration = TimeSpan.FromSeconds(0.5);
            fadeGrid.Target = "Opacity";

            ElementCompositionPreview.SetImplicitShowAnimation(grid, fadeGrid);
            ElementCompositionPreview.SetImplicitShowAnimation(coordStack, fadeGrid);
        }

        void InitCanvas()
        {
            /*
            if (controller == null)
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
            */
        }

        private void OnBackClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Widgets));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("back", Test);
        }
    }
}
