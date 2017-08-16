using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace rMind.Elements
{
    using Draw;
    using Nodes;
    using Types;
    /// <summary> Ending of wire </summary>
    public class rMindBaseWireDot : rMindItemUI, IDrawElement
    {
        rMindBaseWire m_parent;
        public rMindBaseWire Wire { get { return m_parent; } }

        rMindBaseNode m_node = null;
        public rMindBaseNode Node { get { return m_node; } }

        protected Shape m_area;

        public rMindBaseWireDot(rMindBaseWire parent) 
        {
            m_parent = parent;
            Init();
        }

        public virtual void Init()
        {
            m_area = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 12,
                Height = 12,
                Margin = new Windows.UI.Xaml.Thickness(-6),
                IsHitTestVisible = false
            };
            m_template.Children.Add(m_area);
            SubscribeInput();
        }

        public rMindBaseController GetController()
        {
            return m_parent?.GetController();
        }

        public Vector2 GetOffset()
        {            
            return new Vector2(0, 0);            
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
            // e.Handled = true;           
        }

        private void onPointerPress(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            Detach();
            GetController().SetDragWireDot(this, e);
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

        public override Vector2 SetPosition(Vector2 vector)
        {
            var pos = base.SetPosition(vector);
            Wire.Update();
            return pos;
        }

        public rMindBaseWireDot SetNode(rMindBaseNode node)
        {
            m_node = node;
            if (node != null)
                Wire.Update();

            return this;
        }

        public void Detach()
        {
            Node?.Detach(this);
        }

        public override void SetEnabledHitTest(bool state)
        {
            m_area.IsHitTestVisible = state;
        }
    }
}
