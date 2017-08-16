﻿using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Media;
using Windows.Foundation;

namespace rMind.Elements
{
    using Draw;
    using Types;
    /// <summary> Wire to connect nodes </summary>
    public class rMindBaseWire : IDrawElement
    {
        protected rMindBaseWireDot m_a_dot;
        protected rMindBaseWireDot m_b_dot;

        Polyline m_line;

        public rMindBaseWireDot A { get { return m_a_dot; } }
        public rMindBaseWireDot B { get { return m_b_dot; } }
        public Polyline Line { get { return m_line; } }

        rMindBaseController m_parent;

        public rMindBaseWire(rMindBaseController parent)
        {
            m_parent = parent;

            m_a_dot = new rMindBaseWireDot(this);
            m_b_dot = new rMindBaseWireDot(this);

            m_line = new Polyline()
            {                
                Stroke = new SolidColorBrush(Windows.UI.Colors.Blue),
                StrokeThickness = 2,
                IsHitTestVisible = false
            };
        }

        public rMindBaseController GetController()
        {
            return m_parent;         
        }

        public void Init()
        {

        }

        /// <summary> Update line </summary>
        public virtual void Update()
        {
            var points = new PointCollection();
            points.Add(new Point(A.Position.X, A.Position.Y));
            points.Add(new Point(B.Position.X, B.Position.Y));

            m_line.Points = points;
        }

        
        public Vector2 GetOffset() { return new Vector2(0, 0); }

        public void Delete()
        {
            A.Detach();
            B.Detach();

            GetController()?.RemoveWire(this);
        }   
        
        public void SetEnabledHitTest(bool state)
        {
            Line.IsHitTestVisible = state;
            A.SetEnabledHitTest(state);
            B.SetEnabledHitTest(state);
        }
    }
}