using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Content
{
    using Elements;
    using Quad;

    public struct NodeOffset
    {
        public int Column;
        public int Row;
    }
    /// <summary>
    /// Контейнер с нодами, по 4-ем сторонам
    /// </summary>
    public class rMindQuadContainer : rMindBaseElement
    {
        protected List<rMindVerticalLine> m_vertical_lines;
        protected List<rMindHorizontalLine> m_horizontal_lines;

        public rMindQuadContainer(rMindBaseController parent) : base(parent)
        {
            m_vertical_lines = new List<rMindVerticalLine>();
            m_horizontal_lines = new List<rMindHorizontalLine>();
        }

        public override void Init()
        {
            base.Init();
            m_base.Width = 200;
            m_base.Height = 200;
        }

        /// <summary>
        /// Возвращает 
        /// </summary>
        public NodeOffset GetNodeOffset()
        {
            return new NodeOffset
            {
                Row = m_vertical_lines.Max(x => x.TopNodes.Count),
                Column = m_horizontal_lines.Max(x => x.LeftNodes.Count)
            };
        }

        public rMindLine CreateLine<T>()
        {
            if (typeof(T) == typeof(rMindHorizontalLine))
                return CreateHorizontalLine();

            if (typeof(T) == typeof(rMindVerticalLine))
                return CreateVerticalLine();

            return null;
        }

        protected virtual rMindVerticalLine CreateVerticalLine()
        {
            return null;
        }

        protected virtual rMindVerticalLine CreateHorizontalLine()
        {
            return null;
        }
    }
}
