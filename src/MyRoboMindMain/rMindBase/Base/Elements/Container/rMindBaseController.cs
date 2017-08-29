using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

using rMind.Types;

namespace rMind.Elements
{
    using Types;
    using Nodes;

    /// <summary>
    /// Base scheme controller
    /// </summary>
    public partial class rMindBaseController
    {
        public bool CheckIsOvered(rMindBaseElement item)
        {
            return m_overedItem == item;
        }

        public bool CheckIsDraged(rMindBaseElement item)
        {
            return m_items_state.DragedItem == item;
        }

        public bool CheckIsDraggedDot(rMindBaseWireDot dot)
        {
            return m_items_state.DragedWireDot == dot;
        }

        /// <summary>
        /// Add new element
        /// </summary>
        public virtual rMindBaseElement AddElement(rMindBaseElement item)
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
        public virtual void RemoveElement(rMindBaseElement item)
        {
            if (CheckIsOvered(item))
                m_overedItem = null;

            if (m_items.Contains(item))
            {
                if (m_selectedItems.Contains(item))
                    m_selectedItems.Remove(item);

                m_canvas.Children.Remove(item.Template);
                m_items.Remove(item);
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

        protected virtual void DrawElements()
        {
            if (!m_subscribed)
                return;

            foreach (var e in m_items)
                Draw(e);

            foreach (var w in m_wire_list)
                Draw(w);
        }
    }
}

