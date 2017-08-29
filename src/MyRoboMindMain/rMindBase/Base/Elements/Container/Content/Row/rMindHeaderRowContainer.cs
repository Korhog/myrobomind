using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;


namespace rMind.Content
{
    using Elements;
    using rMind.Content.Row;
    using rMind.ColorContainer;

    public class rMindHeaderRowContainer : rMindRowContainer
    {
        Border m_header_rect;

        public struct rMindHeaderRowContainerTheme
        {
            public Color MainColor;
            public Color BorderColor;
            public Color HeaderColor;
        }

        public Color HeaderColor {
            get
            {
                return (m_header_rect.Background as SolidColorBrush).Color;
            }
            set
            {
                /*
                var shades = ColorForge.ColorHelper.GetColorShades(m_accent_color, 6);
                m_header_rect.Background = rMindColors.GetInstance().GetSolidBrush(shades[3]);
                m_header_rect.BorderBrush = rMindColors.GetInstance().GetSolidBrush(shades[2]);
                */
            }
        }

        TextBlock m_header_label;

        public string Header {
            get { return m_header_label.Text; }
            set { m_header_label.Text = value; }
        }


        public rMindHeaderRowContainer(rMindBaseController parent) : base(parent)
        {

        }

        public override void Init()
        {
            base.Init();
            var colors = rMindColors.GetInstance();

            m_header_rect = new Border()
            {
                IsHitTestVisible = false,
                Background = new SolidColorBrush(Colors.Black),
                MinHeight = 32
            };

            Grid.SetColumnSpan(m_header_rect, 3);           

            Template.Children.Add(m_header_rect);

            // Заголовок
            m_header_label = new TextBlock()
            {
                Text = "Header",
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily("Segoe UI"),
                Foreground = colors.GetSolidBrush(Colors.White),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                IsHitTestVisible = false
            };

            Grid.SetColumn(m_header_label, 1);

            Template.Children.Add(m_header_label);

            HeaderColor = Colors.IndianRed;
        }

        protected override int GetRowIndex(rMindRow row)
        {
            return base.GetRowIndex(row) + 1;
        }

        protected override int GetBaseRowSpan()
        {
            return base.GetBaseRowSpan() + 1;
        }

        protected override void SetBorderRadius(CornerRadius value)
        {
            base.SetBorderRadius(value);
            m_header_rect.CornerRadius = new CornerRadius( value.TopLeft, value.TopRight, 0, 0 );
        }

        protected override void SetAccentColor(Color color)
        {
            base.SetAccentColor(color);
            var shades = ColorForge.ColorHelper.GetColorShades(m_accent_color, 8);

            m_header_rect.Background = rMindColors.GetInstance().GetSolidBrush(shades[6]);            
            m_header_rect.BorderBrush = rMindColors.GetInstance().GetSolidBrush(shades[5]);
            m_header_label.Foreground = rMindColors.GetInstance().GetSolidBrush(shades[3]);            
        }

        protected override void SetBorderThickness(Thickness value)
        {
            base.SetBorderThickness(value);
            m_header_rect.BorderThickness = new Thickness(
                value.Left, value.Top, value.Right, 0
            );
        }
    }
}
