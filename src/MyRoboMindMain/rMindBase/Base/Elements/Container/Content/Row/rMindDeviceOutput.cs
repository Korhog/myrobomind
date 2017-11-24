using rMind.Elements;
using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using System.Xml.Linq;

namespace rMind.Content
{
    public class rMindDeviceOutput : rMindRowContainer
    {
        Border content;

        public rMindDeviceOutput(rMindBaseController parent) : base(parent)
        {
            m_element_type = rElementType.RET_DEVICE_OUTPUT;

            AddSetter();
            AddSetter();

            var colors = rMind.ColorContainer.rMindColors.GetInstance();

            content = new Border
            {
                IsHitTestVisible = false,
                Margin = new Windows.UI.Xaml.Thickness(4, 0, 0, 0),
                CornerRadius = new Windows.UI.Xaml.CornerRadius(0, 4, 4, 0),
                Background = colors.GetSolidBrush((Color)ColorHelper.FromArgb(255, 51, 51, 51)),
                Child = new FontIcon()
                {
                    Glyph = "\uE964",
                    FontSize = 32,
                    Foreground = colors.GetSolidBrush(Colors.White)
                }
            };

            Grid.SetColumn(content, 1);
            Grid.SetColumnSpan(content, 2);
            Grid.SetRowSpan(content, 2);
            
            // #2E4DA0
            AccentColor = (Color)ColorHelper.FromArgb(255, 46, 77, 160);
            SetBorderRadius(new Windows.UI.Xaml.CornerRadius(4));

            Template.UseLayoutRounding = true;
            Template.Children.Add(content);
        }

        protected void AddSetter()
        {
            var row = new Row.rMindRow()
            {
                Content = null,
                InputNodeType = rMind.Nodes.rMindNodeConnectionType.Value,
                OutputNodeType = rMind.Nodes.rMindNodeConnectionType.None
            };
            
            AddRow(row);            
        }

        protected override XElement SerializeRows()
        {
            return null;            
        }
    }
}
