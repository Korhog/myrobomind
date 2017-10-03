using rMind.Draw;

using System;
using System.Xml.Linq;

namespace rMind.Elements
{
    using Storage;
    using ColorContainer;
    using Nodes;
    /// <summary>
    /// Base scheme element 
    /// </summary>
    public partial class rMindBaseElement : rMindBaseItem, IDrawContainer, IStorageObject
    {
        public virtual XElement Serialize()
        {
            var itemNode = new XElement("item");
            // attributes
            itemNode.Add(new XAttribute("x", Math.Round(Position.X)));
            itemNode.Add(new XAttribute("y", Math.Round(Position.Y)));

            itemNode.Add(OptionsNode());

            if (m_inner_controller != null)
            {
                itemNode.Add(m_inner_controller.Serialize());
            }

            return itemNode;
        }

        protected virtual XElement OptionsNode()
        {
            var optionsNode = new XElement("options");
            return optionsNode;
        }
    }
}
