using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace rMind.Elements
{
    using Types;
    using Nodes;

    public struct rMindControllesState
    {        
        public rMindBaseElement DragedItem;
        public Vector2 StartPosition;
        public Vector2 StartPointerPosition;
        public bool IsDrag() { return DragedItem != null; }

        public rMindBaseNode OveredNode;
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
        ScaleTransform m_scale;

        // Controls
        rMindControllesState m_items_state;
        List<rMindBaseElement> m_selectedItems;
        rMindBaseElement m_overedItem;

        // Menu
        MenuFlyout m_flyout;        

        public rMindBaseController()
        {
            m_items_state = new rMindControllesState()
            {
                DragedItem = null
            };

            m_items = new List<rMindBaseElement>();
            m_wire_list = new List<rMindBaseWire>();
            m_selectedItems = new List<rMindBaseElement>();
        }  

        /// <summary>
        /// Subscribe to canvas
        /// </summary>
        public virtual void Subscribe(Canvas canvas, ScrollViewer scroll, ScaleTransform scale = null)
        {
            if (m_subscribed)
                Unsubscribe();

            m_canvas = canvas;
            m_scroll = scroll;
            m_scale = scale;

            ResroteControllerState();

            m_subscribed = true;

            SubscribeInput();
            InitMenu();
            DrawElements();
        }

        protected virtual void ResroteControllerState()
        {
            m_scroll.ChangeView(
                -4000,
                3000,
                1);
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
            Vector2 offset = new Vector2(p.Position.X, p.Position.Y) - m_items_state.StartPointerPosition;
            var item = m_items_state.DragedWireDot;
            item.SetPosition(m_items_state.StartPosition + offset);
        }
    }
}

