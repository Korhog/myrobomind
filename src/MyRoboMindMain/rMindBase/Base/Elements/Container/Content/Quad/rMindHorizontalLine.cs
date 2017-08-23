using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
