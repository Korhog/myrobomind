using System.Linq;
using System.Collections.Generic;    

using Windows.UI;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
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

    public enum rMindNodeAttachMode
    {
        Single,
        Multi
    }

    public class rMindBaseNode : rMindItemUI, IDrawElement
    {
        rMindNodeType m_node_type = rMindNodeType.None;
        rMindNodeOriantation m_node_orientation = rMindNodeOriantation.None;
        rMindNodeAttachMode m_attach_mode = rMindNodeAttachMode.Single;

        public rMindNodeAttachMode AttachMode { get { return m_attach_mode; } set { m_attach_mode = value; } }

        rMindBaseElement m_parent;
        public rMindBaseElement Parent { get { return m_parent; } }

        protected Shape m_area;        

        public rMindBaseNode(rMindBaseElement parent) : base()
        {            
            m_parent = parent;
            m_attached_dots = new List<rMindBaseWireDot>();
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
                StrokeThickness = 2
            };

            m_template.Margin = new Windows.UI.Xaml.Thickness(2);
            m_template.Children.Add(m_area);
            SubscribeInput();
        }

        public rMindBaseController GetController()
        {
            return m_parent.GetController();
        }

        public Types.Vector2 GetOffset()
        {
            var localOffset = Parent.GetOffset();

            var rd = Parent.Template.RowDefinitions;
            var h = rd.Where(row => rd.IndexOf(row) < Grid.GetRow(Template))
                .Select(row => row.ActualHeight)
                .Sum() + rd[Grid.GetRow(Template)].ActualHeight / 2;

            var cd = Parent.Template.ColumnDefinitions;
            var w = cd.Where(col => cd.IndexOf(col) < Grid.GetColumn(Template))
                .Select(col => col.ActualWidth)
                .Sum() + cd[Grid.GetColumn(Template)].ActualWidth / 2;

            return localOffset + new Types.Vector2(w, h);
        }

        #region input        
        private void onPointerEnter(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            GetController().SetOveredNode(this);

            if (m_attach_mode == rMindNodeAttachMode.Single && m_attached_dots.Count > 0)
                return;

            Glow(true);
        }


        private void onPointerExit(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            GetController().SetOveredNode(null);
            Glow(false);
        }

        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {
            // e.Handled = true;
        }

        private void onPointerPress(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
            var wire = GetController()?.CreateWire();
            if (wire != null)
            {
                Attach(wire.A);
                GetController()?.SetDragWireDot(wire.B, e);                
            }
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

        #region attached dots

        List<rMindBaseWireDot> m_attached_dots;

        protected virtual bool ValidateAttach(rMindBaseWireDot dot)
        {
            if (m_attach_mode == rMindNodeAttachMode.Single && m_attached_dots.Count > 0)
                return false;

            return true;
        }
        /// <summary> Attach wire dot </summary>
        /// <param name="dot">rMindBaseWireDot</param>
        public void Attach(rMindBaseWireDot dot)
        {
            if (!ValidateAttach(dot))
            {
                dot.Wire.Delete();
                return;
            }

            m_attached_dots.Add(dot.SetNode(this));
            Update();            
        }

        /// <summary> Detach wire dot </summary>
        /// <param name="dot">rMindBaseWireDot</param>
        public void Detach(rMindBaseWireDot dot)
        {
            m_attached_dots.Remove(dot);
        }

        /// <summary> Update attached dots </summary>
        public void Update()
        {
            foreach (var dot in m_attached_dots)
            {
                dot.SetPosition(GetOffset());
            }
        }

        #endregion


        public void SetCell(int col, int row)
        {
            Grid.SetColumn(Template, col);
            Grid.SetRow(Template, row);
        }
    }

}
