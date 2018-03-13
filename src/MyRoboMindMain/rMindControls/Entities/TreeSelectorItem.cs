using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Controls.Entities
{
    public class TreeSelectorItem
    {
        public string Name { get; set; }
        public object Object { get; set; }
        public bool Folder { get { return Children.Count > 0; } }

        public TreeSelectorItem Parent { get; private set; }

        public List<TreeSelectorItem> Children { get; private set; }

        public TreeSelectorItem(TreeSelectorItem parent = null)
        {
            Parent = parent;
            Children = new List<TreeSelectorItem>();
        }
    }
}
