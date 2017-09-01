using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Input;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace rMind.Elements
{
    using Types;
    using Nodes;

    public enum rMindManipulationMode
    {
        None,
        Scale,
        Scroll
    }

    public struct ScaleData
    {
        public Vector2 BeginVector;
        public double BaseScale;
    }

    /// <summary>
    /// Base scheme controller : input
    /// </summary>
    public partial class rMindBaseController
    {
        protected Dictionary<uint, Vector2> m_touch_list;
        protected rMindManipulationMode m_manipulation_mode = rMindManipulationMode.None;
        protected ScaleData m_scale_data;

        TextBlock m_test;

        public void SubscribeInput()
        {
            if (m_touch_list == null)
                m_touch_list = new Dictionary<uint, Vector2>();

            m_test = new TextBlock()
            {
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center
            };
            m_canvas.Children.Add(m_test);


            // events            
            m_canvas.PointerMoved += onPointerMove;

            m_scroll.PointerReleased += onPointerUp;
            m_scroll.PointerPressed += onPointerPress;
            m_scroll.PointerWheelChanged += onWheel;

            m_scroll.PointerExited += onPointerUp;
        }

        public void UnsubscribeInput()
        {
            // events                
            m_canvas.PointerMoved -= onPointerMove;

            m_scroll.PointerReleased -= onPointerUp;
            m_scroll.PointerPressed -= onPointerPress;
            m_scroll.PointerWheelChanged -= onWheel;

            m_scroll.PointerExited -= onPointerUp;
        }            

        // input
        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(m_canvas);

            if (point.PointerDevice.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch && m_touch_list.ContainsKey(point.PointerId))
            {
                m_touch_list.Remove(point.PointerId);
                if (m_manipulation_mode == rMindManipulationMode.Scale && m_touch_list.Count < 2)
                {
                    m_manipulation_mode = rMindManipulationMode.None;
                }
            }


            if (point.Properties.PointerUpdateKind == PointerUpdateKind.RightButtonReleased)
            {

                m_flyout?.ShowAt(m_canvas, point.Position);
                return;
            }

            if (m_overedItem == null && e.KeyModifiers != Windows.System.VirtualKeyModifiers.Shift)
            {
                SetSelectedItem(null);   
            }

            if (point.Properties.PointerUpdateKind == PointerUpdateKind.MiddleButtonReleased)
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

        private void onPointerPress(object sender, PointerRoutedEventArgs e)
        {
            var point = e.GetCurrentPoint(m_scroll);
            if (point.PointerDevice.PointerDeviceType == Windows.Devices.Input.PointerDeviceType.Touch && !m_touch_list.ContainsKey(point.PointerId))
            {
                m_touch_list[point.PointerId] = new Vector2(point.Position.X, point.Position.Y);
                if (m_touch_list.Count > 1)
                {
                    m_manipulation_mode = rMindManipulationMode.Scale;
                    var list = m_touch_list.Values
                        .Take(2)                   
                        .ToList();

                    m_scale_data.BeginVector = list[0] - list[1];
                    m_scale_data.BaseScale = m_scale.ScaleX;
                }
            }
        }

        private void onPointerMove(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;

            if (m_manipulation_mode == rMindManipulationMode.Scale)
            {
                var point = e.GetCurrentPoint(m_scroll);
                if (m_touch_list.ContainsKey(point.PointerId))
                    m_touch_list[point.PointerId] = new Vector2(point.Position.X, point.Position.Y);

                var vectors = m_touch_list.Values
                    .Take(2)
                    .ToList();

                if (vectors.Count == 2)
                {
                    var vec = vectors[1] - vectors[0];
                    var size = Vector2.Length(vec);
                    var k = size / Vector2.Length(m_scale_data.BeginVector);

                    var scale = m_scale_data.BaseScale * k;
                    m_scale.ScaleX = scale;
                    m_scale.ScaleY = scale;                    
                }           
                return;
            }

            if (m_items_state.IsDragDot())
            {
                DragWireDot(e);
            }


            if (m_items_state.IsDrag())
            {
                DragContainer(e);
            }
        }

        private void onWheel(object sender, PointerRoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}

