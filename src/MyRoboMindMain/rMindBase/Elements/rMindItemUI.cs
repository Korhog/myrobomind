using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace rMind.Elements
{
    using Types;

    public class rMindItemUI
    {
        protected Vector2 m_position;
        public Vector2 Position { get { return m_position; } }

        // Graphics
        protected Grid m_template; // main container for any elements
        public Grid Template { get { return m_template; } }

        public rMindItemUI()
        {
            m_template = new Grid()
            {
                UseLayoutRounding = true,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            m_template.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            m_template.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }

        public virtual Vector2 SetPosition(Vector2 newPos)
        {
            var translation = newPos - m_position;
            m_position = newPos;
            SetPosition(newPos.X, newPos.Y);
            return translation;
        }

        public virtual void SetPosition(double x, double y)
        {
            Canvas.SetLeft(m_template, x);
            Canvas.SetTop(m_template, y);
        }

        public virtual void Translate(Vector2 vector)
        {
            SetPosition(Position + vector);
        }

        protected virtual void Glow(bool state)
        {

        }

        public string IDS;
    }
}
