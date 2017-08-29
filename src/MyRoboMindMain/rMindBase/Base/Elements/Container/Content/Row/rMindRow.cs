using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;

namespace rMind.Content.Row
{
    using Nodes;

    public class rMindRow
    {
        public rMindNodeConnectionType InputNodeType { get; set; } = rMindNodeConnectionType.Container;
        public rMindNodeConnectionType OutputNodeType { get; set; } = rMindNodeConnectionType.Container;

        public rMindBaseNode InputNode { get; set; }
        public rMindBaseNode OutputNode { get; set; }

        Button m_delete_button;

        public Button DeleteButton {
            get
            {
                if (m_delete_button == null)
                {
                    m_delete_button = CreateDeleteButton();
                }

                return m_delete_button;
            }
        }

        public FrameworkElement Content { get; set; }

        private Button CreateDeleteButton()
        {
            return new Button()
            {
                Content = new FontIcon()
                {
                    FontFamily = new FontFamily("Segoe MDL2 Assets"),
                    Glyph = "\uE106",
                    Foreground = new SolidColorBrush(Colors.White),
                    FontSize = 10
                },
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(Colors.Red),
                Padding = new Thickness(2),
                Margin = new Thickness(2),
                Tag = this
            };
        }

        public virtual void SetRowIndex(int index)
        {
            if (m_delete_button != null) Grid.SetRow(m_delete_button, index);
            InputNode?.SetCell(InputNode.Column, index);
            OutputNode?.SetCell(OutputNode.Column, index);

            if (Content != null) Grid.SetRow(Content, index);
        } 
    }
}
