using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace rMind.Content
{
    using Elements;
    using Quad;

    public struct NodeOffset
    {
        public int Column;
        public int Row;

        public int VLines;
        public int HLines;

    }
    /// <summary>
    /// Контейнер с нодами, по 4-ем сторонам
    /// </summary>
    public class rMindQuadContainer : rMindBaseElement
    {
        protected List<rMindVerticalLine> m_vertical_lines;
        protected List<rMindHorizontalLine> m_horizontal_lines;

        public List<rMindVerticalLine> VLines { get { return m_vertical_lines; }}
        public List<rMindHorizontalLine> HLines { get { return m_horizontal_lines; } }

        public rMindQuadContainer(rMindBaseController parent) : base(parent)
        {
            m_vertical_lines = new List<rMindVerticalLine>();
            m_horizontal_lines = new List<rMindHorizontalLine>();
        }

        public override void Init()
        {
            base.Init();
            //m_base.Width = 200;
            //m_base.Height = 200;
        }

        /// <summary>
        /// Возвращает 
        /// </summary>
        public NodeOffset GetNodeOffset()
        {
            return new NodeOffset
            {
                Row = m_vertical_lines.Count == 0 ? 0 : m_vertical_lines.Max(x => x.TopNodes.Count),
                Column = m_horizontal_lines.Count == 0 ? 0 : m_horizontal_lines.Max(x => x.LeftNodes.Count),
                HLines = m_horizontal_lines.Count,
                VLines = m_vertical_lines.Count                
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
            var line = new rMindVerticalLine(this);

            bool first = m_vertical_lines.Count == 0;
            if (!first)
            {
                Template.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                foreach(var l in HLines)
                {
                    foreach (var node in l.RightNodes)
                        node.Column += 1;
                }
            }

            m_vertical_lines.Add(line);
            UpdateBase();
            return line;
        }

        protected virtual rMindHorizontalLine CreateHorizontalLine()
        {
            var line = new rMindHorizontalLine(this);

            bool first = m_horizontal_lines.Count == 0;
            if(!first)
            {
                Template.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                foreach (var l in VLines)
                {
                    foreach (var node in l.BottomNodes)
                        node.Row += 1;
                }
            }

            m_horizontal_lines.Add(line);
            UpdateBase();
            return line;
        }

        public void RemoveLine(rMindLine line)
        {

        }


        protected virtual void RemoveVerticalLine()
        {

        }

        protected virtual void RemoveHorizontalLine()
        {

        }

        public virtual int GetLineIndex(rMindLine line)
        {
            if (line is rMindVerticalLine)
            {
                return m_vertical_lines.IndexOf(line as rMindVerticalLine) + GetNodeOffset().Column;
            }

            if (line is rMindHorizontalLine)
            {
                return m_horizontal_lines.IndexOf(line as rMindHorizontalLine) + GetNodeOffset().Row;
            }

            return 0;
        }

        public void UpdateBase()
        {
            var offset = GetNodeOffset();
            Grid.SetColumn(m_base, offset.Column);
            Grid.SetRow(m_base, offset.Row);

            Grid.SetColumnSpan(m_base, offset.VLines == 0 ? 1 : offset.VLines);
            Grid.SetRowSpan(m_base, offset.HLines == 0 ? 1 : offset.HLines);
        }
    }
}
