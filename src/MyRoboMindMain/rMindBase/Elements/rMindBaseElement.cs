using rMind.Draw;
using rMind.Types;
using rMind.Theme;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using System;

namespace rMind.Elements
{
    /// <summary>
    /// Base scheme element 
    /// </summary>
    public class rMindBaseElement : IDrawContainer
    {
        rMindBaseController m_parent;  
        public rMindBaseController Parent { get { return m_parent; } }

        // Graphics
        protected Grid m_template; // main container for any elements
        public Grid Template { get { return m_template; } }

        // Properties
        public Vector2 m_position;
        public Vector2 Position { get { return m_position; } }


        public rMindBaseElement(rMindBaseController parent)
        {
            m_parent = parent;
            m_template = new Grid
            {
                Width = 80,
                Height = 50,
                
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            SubscribeInput();
            Init();
        }

        public void Delete()
        {
            m_parent.Remove(this);
        }

        public virtual void Init()
        {
            Template.Background = rMindScheme.Get().MainContainerBrush();
            Template.BorderBrush = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Colors.Black);
        }

        public virtual void SetPosition(Vector2 newPos)
        {
            m_position = newPos;
            SetPosition(newPos.X, newPos.Y);
        }

        public virtual void SetPosition(float x, float y)
        {
            Canvas.SetLeft(m_template, x);
            Canvas.SetTop(m_template, y);
        }

        public void Translate(Vector2 vector)
        {
            SetPosition(Position + vector);
        }

        // input 
        private void onPointerEnter(object sender, PointerRoutedEventArgs e)
        {
            Parent.SetOveredItem(this);
            Template.BorderThickness = new Thickness(4);
        }

        private void onPointerExit(object sender, PointerRoutedEventArgs e)
        {
            Parent.SetOveredItem(null);
            Template.BorderThickness = new Thickness(0);
        }

        private void onPointerUp(object sender, PointerRoutedEventArgs e)
        {
            if (Parent.CheckIsOvered(this))
            {
                SetSelected(true);
                Parent.SetSelectedItem(this, e.KeyModifiers == Windows.System.VirtualKeyModifiers.Shift);
            }
        }

        void SubscribeInput()
        {
            Template.PointerEntered += onPointerEnter;
            Template.PointerExited += onPointerExit;
            Template.PointerReleased += onPointerUp;
        }

        public void SetSelected(bool state)
        {
            if (state)
                Template.BorderThickness = new Thickness(8);            
            else
                Template.BorderThickness = new Thickness(4);
        }
    }
}
