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

    public enum rMindNodeConnectionType
    {
        None,
        Container,
        Value
    }

    public struct rMindNodeDesc
    {
        public rMindNodeType NodeType;
        public rMindNodeOriantation NodeOrientation;
        public rMindNodeAttachMode AttachMode;
        public rMindNodeConnectionType ConnectionType;
    }

    public class rMindBaseNode : rMindItemUI, IDrawElement
    {
        int m_row = 0;
        int m_col = 0;

        public int Column {
            get
            {
                return m_col;
            }
            set
            {
                m_col = value;
                Grid.SetColumn(m_template, m_col);
            }
        }

        public int Row
        {
            get
            {
                return m_row;
            }
            set
            {
                m_row = value;
                Grid.SetRow(m_template, m_row);
            }
        }

        rMindNodeType m_node_type = rMindNodeType.None;
        rMindNodeOriantation m_node_orientation = rMindNodeOriantation.None;
        rMindNodeAttachMode m_attach_mode = rMindNodeAttachMode.Single;
        rMindNodeConnectionType m_connection_type = rMindNodeConnectionType.Container;

        public rMindNodeAttachMode AttachMode { get { return m_attach_mode; } set { m_attach_mode = value; } }
        public rMindNodeConnectionType ConnectionType {
            get { return m_connection_type; }
            set { SetConnectionType(value); }
        }

        protected virtual void SetConnectionType(rMindNodeConnectionType connectionType)
        {
            if (m_connection_type == connectionType)
                return;

            m_connection_type = connectionType;
            var r = m_connection_type == rMindNodeConnectionType.Container ? 10 : 3;

            m_area.RadiusX = r;
            m_area.RadiusY = r;
        }

        public rMindNodeOriantation NodeOrientation
        {
            get { return m_node_orientation; }
            set { SetNodeOrientation(value); }
        }

        protected virtual void SetNodeOrientation(rMindNodeOriantation orientation)
        {
            m_node_orientation = orientation;
            Windows.UI.Xaml.Thickness thickness;
            switch(orientation)
            {
                case rMindNodeOriantation.Left:
                    thickness = new Windows.UI.Xaml.Thickness(2, 6, 2, 6);
                    break;
                case rMindNodeOriantation.Right:
                    thickness = new Windows.UI.Xaml.Thickness(2, 6, 2, 6);
                    break;
                case rMindNodeOriantation.Top:
                    thickness = new Windows.UI.Xaml.Thickness(6, 2, 6, 2);
                    break;
                case rMindNodeOriantation.Bottom:
                    thickness = new Windows.UI.Xaml.Thickness(6, 2, 6, 2);
                    break;
                default:
                    thickness = new Windows.UI.Xaml.Thickness(2, 6, 2, 6);
                    break;
            }

            m_template.Margin = thickness;
        }

        rMindBaseElement m_parent;
        public rMindBaseElement Parent { get { return m_parent; } }

        protected Rectangle m_area;        

        public rMindBaseNode(rMindBaseElement parent) : base()
        {            
            m_parent = parent;
            m_attached_dots = new List<rMindBaseWireDot>();
            Init();
        }

        public void Init()
        {
            var r = m_connection_type == rMindNodeConnectionType.Container ? 10 : 3;

            m_area = new Rectangle {
                Width = 20,
                Height = 20,
                Stroke = new SolidColorBrush(Colors.Black),
                Fill = new SolidColorBrush(Colors.DarkGray),
                RadiusX = r,
                RadiusY = r,
                StrokeThickness = 2
            };

            m_template.Margin = new Windows.UI.Xaml.Thickness(2, 6, 2, 6);
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

        /// <summary> Отсоединение всех узлов </summary>        
        public void Detach()
        {
            while(m_attached_dots.Count > 0)            
                Detach(m_attached_dots[0]);
        }

        /// <summary> Detach wire dot </summary>
        /// <param name="dot">rMindBaseWireDot</param>
        public void Detach(rMindBaseWireDot dot)
        {
            dot.Detach();
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
            Column = col;
            Row = row;
            Update();
        }
    }

}
