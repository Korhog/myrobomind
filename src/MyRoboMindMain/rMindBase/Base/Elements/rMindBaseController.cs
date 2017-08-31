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

            // events            
            m_canvas.PointerMoved += onPointerMove;

            m_scroll.PointerReleased += onPointerUp;
            m_scroll.PointerWheelChanged += onWheel;

            m_scroll.PointerExited += onPointerUp;


            m_subscribed = true;

            InitMenu();
            DrawElements();
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
                m_canvas.PointerMoved -= onPointerMove;

                m_scroll.PointerReleased -= onPointerUp;
                m_scroll.PointerWheelChanged -= onWheel;

                m_scroll.PointerExited -= onPointerUp;

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

        // input
        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(m_canvas);
            if (point.Properties.PointerUpdateKind == Windows.UI.Input.PointerUpdateKind.RightButtonReleased)
            {

                m_flyout?.ShowAt(m_canvas, point.Position);
                return;
            }


            if (m_overedItem == null && e.KeyModifiers != Windows.System.VirtualKeyModifiers.Shift)
            {
                SetSelectedItem(null);   
            }

            if (point.Properties.PointerUpdateKind == Windows.UI.Input.PointerUpdateKind.MiddleButtonReleased)
            {
                if (m_overedItem != null)
                {
                    m_overedItem.Delete();
                }
            }


            if (m_items_state.IsDragDot())
            { 
                if (m_items_state.OveredNode == null)
                {
                    m_items_state.DragedWireDot.Wire.Delete();
                    m_items_state.DragedWireDot = null;
                }
                else
                {
                    m_items_state.OveredNode.Attach(m_items_state.DragedWireDot);
                    m_items_state.DragedWireDot.Wire.SetEnabledHitTest(true);
                    SetDragWireDot(null, e);
                }
            }

            m_items_state.DragedItem = null;

        }

        private void onPointerMove(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;                 
            if (m_items_state.IsDragDot())
            {
                DragWireDot(e);
            }


            if (m_items_state.IsDrag())
            {
                DragContainer(e);
            }
        }

        protected void DragWireDot(PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(m_canvas);
            Vector2 offset = new Vector2(p.Position.X, p.Position.Y) - m_items_state.StartPointerPosition;
            var item = m_items_state.DragedWireDot;
            item.SetPosition(m_items_state.StartPosition + offset);
        }

        private void onWheel(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}

