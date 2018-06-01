using rMind.CanvasEx;
using rMind.Content;
using rMind.Elements;
using rMind.IoT.NodeEngine;
using rMind.Nodes;
using rMind.SmartHome;
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
using System.Reflection;

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
                    scroll.Visibility = Visibility.Visible;
                    coordStack.Visibility = Visibility.Visible;
                };
                animation.TryStart(Test);
            }

            InitCanvas();
        }

        void CreateAnimations()
        {
            var comp = ElementCompositionPreview.GetElementVisual(this).Compositor;

            scroll.Visibility = Visibility.Collapsed;
            coordStack.Visibility = Visibility.Collapsed;

            var fadeGrid = comp.CreateScalarKeyFrameAnimation();
            fadeGrid.InsertKeyFrame(0, 0f);
            fadeGrid.InsertKeyFrame(1, 1f);
            fadeGrid.Duration = TimeSpan.FromSeconds(0.25);
            fadeGrid.Target = "Opacity";

            ElementCompositionPreview.SetImplicitShowAnimation(scroll, fadeGrid);
            ElementCompositionPreview.SetImplicitShowAnimation(coordStack, fadeGrid);
        }

        void InitCanvas()
        {
            
            if (controller == null)
            {
                controller = new rMindCanvasController(canvas, scroll);
                var main = new IOTController(controller);
                controller.SetController(main);
                controller.Draw();

                main.Flyout.Items.Add(new MenuFlyoutSeparator());

                var item = new MenuFlyoutSubItem
                {
                    Text = "Драйверы",
                };

                var devices = Assembly.GetAssembly(typeof(IIOTDevice))
                    .GetTypes()
                    .Where(x => 
                        typeof(IIOTDevice).IsAssignableFrom(x) && 
                        x.IsClass &&
                        !x.IsAbstract
                    );

                foreach (var device in devices)
                {
                    var name = device.GetCustomAttribute<DisplayName>()?.Value ?? "NONE";

                    var subItem = new MenuFlyoutItem
                    {
                        Text = name
                    };

                    subItem.Click += (s, e) =>
                    {
                        MethodInfo method = main.GetType().GetMethod("Add").MakeGenericMethod(new Type[] { device });
                        method.Invoke(main, null);
                    };

                    item.Items.Add(subItem);
                }

                main.Flyout.Items.Add(item);
            }
            
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
