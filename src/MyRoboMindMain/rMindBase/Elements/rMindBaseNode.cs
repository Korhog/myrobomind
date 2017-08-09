using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace rMind.Nodes
{
    using Draw;
    using Elements;

    public enum rMindNodeType
    {
        None,
        Input,
        Output
    }

    public enum rMindNodeOriantation
    {
        None,
        Left,
        Right,
        Top,
        Bottom
    }

    public class rMindBaseNode : rMindItemUI, IDrawElement
    {
        rMindNodeType m_node_type = rMindNodeType.None;
        rMindNodeOriantation m_node_orientation = rMindNodeOriantation.None;

        rMindBaseElement m_parent;
        public rMindBaseElement Parent { get { return m_parent; } }

        protected Shape m_area;

        public rMindBaseNode(rMindBaseElement parent) : base()
        {            
            m_parent = parent;
            Init();
        }

        public void Init()
        {
            m_area = new Rectangle {
                Width = 20,
                Height = 20,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.DarkGray),
                RadiusX = 0,
                RadiusY = 0,
                StrokeThickness = 1
            };

            m_template.Children.Add(m_area);
            SubscribeInput();
        }

        public rMindBaseController GetController()
        {
            return m_parent.GetController();
        }

        public Types.Vector2 GetOffset()
        {
            return new Types.Vector2(0, 0);
        }

        #region input        
        private void onPointerEnter(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            Glow(true);
        }


        private void onPointerExit(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            Glow(false);
        }

        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void onPointerPress(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            GetController()?.CreateWire();
        }

        void SubscribeInput()
        {
            m_area.PointerEntered += onPointerEnter;
            m_area.PointerExited += onPointerExit;
            m_area.PointerPressed += onPointerPress;
            m_area.PointerReleased += onPointerUp;            
        }

        void UnsubscribeInput()
        {
            m_area.PointerEntered -= onPointerEnter;
            m_area.PointerExited -= onPointerExit;
            m_area.PointerPressed -= onPointerPress;
            m_area.PointerReleased -= onPointerUp;            
        }
        #endregion

        protected override void Glow(bool state)
        {
            m_area.Stroke = state ? new SolidColorBrush(Colors.YellowGreen) : new SolidColorBrush(Colors.Black);
        }

    }

}
