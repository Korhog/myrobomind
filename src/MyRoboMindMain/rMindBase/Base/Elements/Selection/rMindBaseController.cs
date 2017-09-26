using System;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

using rMind.Types;

namespace rMind.Elements
{
    using Types;
    using Nodes;
    

    /// <summary>
    /// selector section of controller 
    /// </summary>
    public partial class rMindBaseController
    {
        Vector2 m_start_point;

        Rectangle m_selector_rect;

        protected virtual Rectangle CreateSelectorRect()
        {
            var rect = new Rectangle()
            {
                Fill = new SolidColorBrush(Colors.Black),
                Width = 20,
                Height = 20,
                Opacity = 0.5,
                IsHitTestVisible = false
            };

            return rect;
        }

        protected Rectangle SelectorRect
        {
            get
            {
                if (m_selector_rect == null)
                {
                    m_selector_rect = CreateSelectorRect();
                }
                return m_selector_rect;
            }
        }
        
        public void StartSelection(PointerPoint p)
        {
            if (!m_subscribed)
                return;

            if (!m_canvas.Children.Contains(SelectorRect))
            {
                m_canvas.Children.Add(SelectorRect);
            }

            m_start_point = new Vector2(p);
            UpdateSelectorRect(p);
            m_manipulation_mode = rMindManipulationMode.Select;
            m_canvas.ManipulationMode = ManipulationModes.None;
        }

        public void StopSelection()
        {
            if (!m_subscribed)
                return;

            if (m_canvas.Children.Contains(SelectorRect))
            {
                m_canvas.Children.Remove(SelectorRect);
            }

            m_manipulation_mode = rMindManipulationMode.None;
            SelectItems();
        }

        public void UpdateSelectorRect(PointerPoint p)
        {
            var pos = new Vector2(p);

            var rect = SelectorRect;

            Canvas.SetLeft(rect, Math.Min(pos.X, m_start_point.X));
            Canvas.SetTop(rect, Math.Min(pos.Y, m_start_point.Y));
            rect.Width = Math.Abs(pos.X - m_start_point.X);
            rect.Height = Math.Abs(pos.Y - m_start_point.Y);
        }

        protected void SelectItems()
        {
            SetSelectedItem(null);
            foreach(var item in m_items.Where(x => x.InRect(SelectorRect)))
            {
                SetSelectedItem(item, true);
            }
        }  
        
        protected virtual void DeleteSelection()
        {
            if (m_selected_items.Count > 0)
            {
                while (m_selected_items.Count > 0)
                {
                    m_selected_items[0].Delete();
                }
                m_selected_items.Clear();
            }
        }
    }
}

