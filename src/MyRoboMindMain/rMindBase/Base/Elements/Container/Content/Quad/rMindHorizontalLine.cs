using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace rMind.Content.Quad
{
    using Nodes;

    public class rMindHorizontalLine : rMindLine
    {
        public rMindHorizontalLine(rMindQuadContainer parent) : base(parent)
        {
            m_line_orientation = rMindLineOrientation.Horizontal;
        }

        public List<rMindBaseNode> LeftNodes { get { return NodesA; } }
        public List<rMindBaseNode> RightNodes { get { return NodesB; } }

        public rMindBaseNode AddLeftNode()
        {
            int currentCount = m_parent.HLines.Max(line => line.LeftNodes.Count);
            if (LeftNodes.Count + 1 > currentCount)
            {
                /*
                 * если количество верхних узлов равно максимальному количеству по линиям
                 * добавляем новую строку.
                 */
                m_parent.Template.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                foreach (var line in m_parent.HLines)
                    line.ShiftNodes(1);

                foreach (var line in m_parent.VLines)
                {
                    foreach (var n in line.TopNodes.Union(line.BottomNodes))
                        n.Column += 1;
                }

                m_parent.UpdateBase();
            }
            else
            {
                ShiftNodes(1);
            }

            var node = m_parent.CreateNode();
            node.SetCell(0, m_parent.GetLineIndex(this));
            LeftNodes.Add(node);

            return null;
        }

        public rMindBaseNode AddBottomNode()
        {
            return null;
        }

        public override void ShiftNodes(int offset)
        {
            foreach (var node in LeftNodes.Union(RightNodes))
                node.Column += 1;
        }
    }

}
