using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace rMind.Elements
{
    using Draw;
    using Types;

    public class rMindBaseItem : rMindItemUI, IDrawElement
    {
        protected rMindBaseController m_parent;
        public rMindBaseController Parent { get { return m_parent; } }

        public rMindBaseItem(rMindBaseController parent)
        {
            m_parent = parent;
            m_template = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };
        }
        
        public virtual void Init()
        {

        }

        public rMindBaseController GetController()
        {
            return m_parent;
        }

        public virtual Vector2 GetOffset()
        {
            return new Vector2(0, 0);
        }
    }
}
