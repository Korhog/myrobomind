using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace rMind.Elements
{
    using Types;
    using Nodes;

    public struct rMindControllerState
    {        
        public rMindBaseElement DragedItem;
        public Vector2 StartPosition;
        public Vector2 StartPointerPosition;
        public bool IsDrag() { return DragedItem != null; }

        public rMindBaseNode OveredNode;
        public rMindBaseNode MagnetNode;

        public rMindBaseWireDot DragedWireDot;
        public bool IsDragDot() { return DragedWireDot != null; }

        public rMindItemUI ActionItem;
    }

    /// <summary>
    /// Base scheme controller
    /// </summary>
    public partial class rMindBaseController
    {
        protected List<rMindBaseElement> m_items;
        protected List<rMindBaseWire> m_wire_list;

        protected bool m_subscribed;

        // Graphics
        Canvas m_canvas;
        ScrollViewer m_scroll;

        // Controls
        rMindControllerState m_items_state;
        List<rMindBaseElement> m_selected_items;
        rMindBaseElement m_overed_item;

        // Menu
        MenuFlyout m_flyout;

        // Ext
        rMindMagnet m_magnet;

        public rMindBaseController()
        {
            m_items_state = new rMindControllerState()
            {
                DragedItem = null
            };

            m_magnet = new rMindMagnet();
            m_items = new List<rMindBaseElement>();
            m_wire_list = new List<rMindBaseWire>();
            m_selected_items = new List<rMindBaseElement>();
        }  

        /// <summary>
        /// Subscribe to canvas
        /// </summary>
        public virtual void Subscribe(Canvas canvas, ScrollViewer scroll)
        {
            if (m_subscribed)
                Unsubscribe();

            m_canvas = canvas;
            m_scroll = scroll;   

            m_subscribed = true;

            m_magnet.Draw(m_canvas);
            SubscribeInput();
            InitMenu();
            DrawElements();
            ResroteControllerState();
        }

        protected virtual void ResroteControllerState()
        {
            // пока смотрим в центр
            onLoad(null, null);
        }

        /// <summary>
        /// Unsubscribe from canvas
        /// </summary>
        public void Unsubscribe()
        {
            if (m_subscribed)
            {
                m_canvas.Children.Clear();
                // events                
                UnsubscribeInput();

                m_canvas = null;
                m_scroll = null;
            }            
            m_subscribed = false;
        }

        protected virtual void Draw(rMindBaseElement item)
        {
            if (m_subscribed && !m_canvas.Children.Contains(item.Template))
            {
                m_canvas.Children.Add(item.Template);
            }
        }

        protected void DragWireDot(PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(m_canvas);
            Vector2 offset = new Vector2(p) - m_items_state.StartPointerPosition;
            var item = m_items_state.DragedWireDot;
            var pos = m_items_state.StartPosition + offset;
            // var seek nodes 
            m_items_state.MagnetNode = BakedNodes
                .Where(pair => Vector2.Length(pair.Key - pos) < (100 / m_scroll.ZoomFactor))
                .OrderBy(pair => Vector2.Length(pair.Key - pos))
                .Select(pair => pair.Value)
                .FirstOrDefault();

            if (m_items_state.MagnetNode == null)
            {
                m_magnet.Hide();
            }
            else
            {
                m_magnet.Show();
                m_magnet.Magnet(pos, m_items_state.MagnetNode.GetOffset());
            }                          

            item.SetPosition(pos);
        }
    }
}

