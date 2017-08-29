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
        
        public Color HeaderColor {
            get
            {
                return (m_header_rect.Background as SolidColorBrush).Color;
            }
            set
            {
                var colors = rMindColors.GetInstance();

                m_header_rect.Background = colors.GetSolidBrush(value);
                m_header_rect.BorderBrush = colors.GetSolidBrush(
                    rMindColors.ColorBrigness(value, 80, false)
                );

                var brigness = rMindColors.Brigness(value);
                m_header_label.Foreground = colors.GetSolidBrush(
                     rMindColors.ColorBrigness(value, 40, brigness < 130)
                );
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
            var colors = rMindColors.GetInstance();
            HeaderColor = rMindColors.ColorBrigness(m_accent_color, 80, false); 
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
