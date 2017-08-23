using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;

namespace rMind.Content
{
    using Elements;
    using rMind.Content.Row;

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
                (m_header_rect.Background as SolidColorBrush).Color = value;
            }
        }

        public rMindHeaderRowContainer(rMindBaseController parent) : base(parent)
        {

        }

        public override void Init()
        {
            base.Init();


            m_header_rect = new Border()
            {
                IsHitTestVisible = false,
                Background = new SolidColorBrush(Colors.Black),
                MinHeight = 32
            };

            Grid.SetColumnSpan(m_header_rect, 3);            

            HeaderColor = Colors.IndianRed;

            Template.Children.Add(m_header_rect);
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
    }
}
