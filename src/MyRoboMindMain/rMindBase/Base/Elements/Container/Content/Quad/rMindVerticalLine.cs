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

    public class rMindVerticalLine : rMindLine
    {
        public rMindVerticalLine(rMindQuadContainer parent) : base(parent)
        {
            m_line_orientation = rMindLineOrientation.Vertical;
        }

        public List<rMindBaseNode> TopNodes { get { return NodesA; } }
        public List<rMindBaseNode> BottomNodes { get { return NodesB; } }

        public rMindBaseNode AddTopNode()
        {
            int currentCount = m_parent.VLines.Max(line => line.TopNodes.Count);
            if (TopNodes.Count + 1 > currentCount)
            {
                /*
                 * если количество верхних узлов равно максимальному количеству по линиям
                 * добавляем новую строку.
                 */
                m_parent.Template.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                foreach (var line in m_parent.VLines)
                    line.ShiftNodes(1);

                foreach (var line in m_parent.HLines)
                {
                    foreach (var n in line.LeftNodes.Union(line.RightNodes))
                        n.Row += 1;
                }

                m_parent.UpdateBase();
            }
            else
            {
                ShiftNodes(1);
            }

            var node = m_parent.CreateNode();
            node.SetCell(m_parent.GetLineIndex(this), 0);
            TopNodes.Add(node);            

            return null;
        }

        public rMindBaseNode AddBottomNode()
        {
            return null;
        }

        public override void ShiftNodes(int offset)
        {
            foreach (var node in TopNodes.Union(BottomNodes))
                node.Row += 1;
        }
    }
}
