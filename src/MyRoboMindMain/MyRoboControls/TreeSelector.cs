using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace MyRoboControls
{
    public sealed class TreeSelector : Control
    {
        Button menuButton;
        ListView items;

        public TreeSelector()
        {
            this.DefaultStyleKey = typeof(TreeSelector);            
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            items = GetTemplateChild("PART_Items") as ListView;

            menuButton = GetTemplateChild("PART_MenuButton") as Button;
            menuButton.Click += (s, e) => {
                if (items.Visibility == Visibility.Collapsed)
                {
                    items.Visibility = Visibility.Visible;
                }
                else
                {

                }
            };
        }
    }
}
