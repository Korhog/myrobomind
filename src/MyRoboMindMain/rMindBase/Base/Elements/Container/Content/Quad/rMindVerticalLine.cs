using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
