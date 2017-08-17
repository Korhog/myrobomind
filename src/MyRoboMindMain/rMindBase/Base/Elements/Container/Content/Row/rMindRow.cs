using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Content.Row
{
    using Nodes;

    public class rMindRow
    {
        public rMindNodeConnectionType InputNodeType { get; set; } = rMindNodeConnectionType.None;
        public rMindNodeConnectionType OutputNodeType { get; set; } = rMindNodeConnectionType.None;
    }
}
