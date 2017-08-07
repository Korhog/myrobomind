using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace rMind.Elements
{
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

        // Controls
        List<rMindBaseElement> m_selectedItems;
        rMindBaseElement m_overedItem;
        rMindBaseElement m_dragedItem;

        public bool CheckIsOvered(rMindBaseElement item)
        {
            return m_overedItem == item;
        }

        public rMindBaseController()
        {
            m_items = new List<rMindBaseElement>();
            m_selectedItems = new List<rMindBaseElement>();
        }

        /// <summary>
        /// Add new element
        /// </summary>
        public virtual rMindBaseElement Add(rMindBaseElement item)
        {
            m_items.Add(item);

            if(m_subscribed)
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
        public virtual void Subscribe(Canvas canvas, ScrollViewer scroll)
        {
            if (m_subscribed)
                Unsubscribe();

            m_canvas = canvas;
            m_scroll = scroll;

            m_subscribed = true;
        }

        /// <summary>
        /// Unsubscribe from canvas
        /// </summary>
        public void Unsubscribe()
        {
            if (m_subscribed)
            {

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
            if (!multi)
            {
                foreach(var it in m_selectedItems)
                {
                    if (it == item)
                        continue;
                    it.SetSelected(false);
                }
            }
            if (!m_selectedItems.Contains(item))
                m_selectedItems.Add(item);
        }

        public void SetOveredItem(rMindBaseElement item)
        {
            m_overedItem = item;
        }
    }
}
