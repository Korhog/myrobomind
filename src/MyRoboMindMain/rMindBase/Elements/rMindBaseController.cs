using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

using rMind.Types;

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
    }

    /// <summary>
    /// Base scheme controller
    /// </summary>
    public class rMindBaseController
    {
        protected List<rMindBaseElement> m_items;
        protected bool m_subscribed;

        // Graphics
        Canvas m_canvas;
        ScrollViewer m_scroll;
        ScaleTransform m_scale;

        // Controls
        rMindControllesState m_items_state;
        List<rMindBaseElement> m_selectedItems;
        rMindBaseElement m_overedItem;

        public bool CheckIsOvered(rMindBaseElement item)
        {
            return m_overedItem == item;
        }

        public bool CheckIsDraged(rMindBaseElement item)
        {
            return m_items_state.DragedItem == item;
        }

        public rMindBaseController()
        {
            m_items_state = new rMindControllesState()
            {
                DragedItem = null
            };

            m_items = new List<rMindBaseElement>();
            m_selectedItems = new List<rMindBaseElement>();
        }

        /// <summary>
        /// Add new element
        /// </summary>
        public virtual rMindBaseElement Add(rMindBaseElement item)
        {
            m_items.Add(item);

            if (m_subscribed)
            {
                Draw(item);
            }

            return item;
        }

        /// <summary>
        /// Remove item from canvas
        /// </summary>
        public virtual void Remove(rMindBaseElement item)
        {
            if (m_items.Contains(item))
                m_items.Remove(item);
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

            m_subscribed = true;
        }

        /// <summary>
        /// Unsubscribe from canvas
        /// </summary>
        public void Unsubscribe()
        {
            if (m_subscribed)
            {
                // events                
                m_canvas.PointerMoved -= onPointerMove;

                m_scroll.PointerReleased -= onPointerUp;
                m_scroll.PointerWheelChanged -= onWheel;

                m_canvas = null;
                m_scroll = null;
            }
            m_subscribed = false;
        }

        protected virtual void Draw(rMindBaseElement item)
        {
            if (m_subscribed)
            {
                m_canvas.Children.Add(item.Template);
            }
        }

        public void SetSelectedItem(rMindBaseElement item, bool multi = false)
        {
            if (item == null)
            {
                foreach (var it in m_selectedItems)
                {
                    it.SetSelected(false);
                }
                m_selectedItems.Clear();
                return;
            }
            if (!multi)
            {
                foreach (var it in m_selectedItems)
                {
                    if (it == item)
                        continue;
                    it.SetSelected(false);
                }
                m_selectedItems.Clear();
                m_selectedItems.Add(item);
            }
            if (!m_selectedItems.Contains(item))
                m_selectedItems.Add(item);
        }

        public void SetOveredItem(rMindBaseElement item)
        {
            m_overedItem = item;
        }

        public void SetOveredNode(rMindBaseNode node)
        {
            m_items_state.OveredNode = node;
        }

        public void SetDragItem(rMindBaseElement item, PointerRoutedEventArgs e)
        {
            m_items_state.DragedItem = item;
            if (item == null)
                return;

            var p = e.GetCurrentPoint(m_canvas);
            m_items_state.StartPointerPosition = new Vector2(p.Position.X, p.Position.Y);
            m_items_state.StartPosition = item.Position;
        }

        public void SetDragWireDot(rMindBaseWireDot item, PointerRoutedEventArgs e)
        {
            m_items_state.DragedWireDot = item;
            if (item == null)
                return;
            
            var p = e.GetCurrentPoint(m_canvas);
            item.SetPosition(new Vector2(p.Position.X - 6, p.Position.Y - 6));
            m_items_state.StartPointerPosition = new Vector2(p.Position.X, p.Position.Y);
            m_items_state.StartPosition = item.Position;
        }

        // input
        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {
            if (m_items_state.IsDragDot() && m_items_state.OveredNode != null)
            {
                m_items_state.OveredNode.Attach(m_items_state.DragedWireDot);
                SetDragWireDot(null, e);
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

        protected void DragContainer(PointerRoutedEventArgs e)
        {
            var p = e.GetCurrentPoint(m_canvas);
            Vector2 offset = new Vector2(p.Position.X, p.Position.Y) - m_items_state.StartPointerPosition;
            var item = m_items_state.DragedItem;

            var translation = item.SetPosition(m_items_state.StartPosition + offset);
            if (m_selectedItems.Contains(item))
            {
                foreach (var it in m_selectedItems)
                {
                    if (it == item)
                        continue;

                    it.Translate(translation);
                }

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

        /// <summary>
        /// Create new wire
        /// </summary>
        public virtual rMindBaseWire CreateWire()
        {
            var wire = new rMindBaseWire(this);
            wire.A.Translate(new Vector2(20, 50));
            wire.B.Translate(new Vector2(10, 10));

            if (m_subscribed)
            {
                m_canvas.Children.Add(wire.Line);
                m_canvas.Children.Add(wire.A.Template);
                m_canvas.Children.Add(wire.B.Template);                
            }

            return wire;
        }
    }
}

