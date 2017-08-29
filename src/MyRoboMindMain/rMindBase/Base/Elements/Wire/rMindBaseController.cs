using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

using rMind.Types;

namespace rMind.Elements
{
    using Types;
    using Nodes;

    public partial class rMindBaseController
    {        
        /// <summary>
        /// Create new wire
        /// </summary>
        public virtual rMindBaseWire CreateWire()
        {
            var wire = new rMindBaseWire(this);
            wire.A.Translate(new Vector2(20, 50));
            wire.B.Translate(new Vector2(10, 10));

            Draw(wire);

            m_wire_list.Add(wire);
            return wire;
        }

        protected virtual void Draw(rMindBaseWire wire)
        {
            if (m_subscribed)
            {
                m_canvas.Children.Add(wire.Line);
                m_canvas.Children.Add(wire.A.Template);
                m_canvas.Children.Add(wire.B.Template);
            }
        }

        public virtual void RemoveWire(rMindBaseWire wire)
        {
            m_canvas.Children.Remove(wire.A.Template);
            m_canvas.Children.Remove(wire.B.Template);
            m_canvas.Children.Remove(wire.Line);
            m_wire_list.Remove(wire);
        }

        public void SetDragWireDot(rMindBaseWireDot item, PointerRoutedEventArgs e)
        {
            m_items_state.DragedWireDot = item;
            if (item == null)
                return;

            item.Wire.SetEnabledHitTest(false);
            var p = e.GetCurrentPoint(m_canvas);
            item.SetPosition(new Vector2(p.Position.X, p.Position.Y));
            m_items_state.StartPointerPosition = new Vector2(p.Position.X, p.Position.Y);
            m_items_state.StartPosition = item.Position;
        }
    }
}

