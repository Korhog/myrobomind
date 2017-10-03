using System.Linq;
using System.Xml;
using System.Xml.Linq;

using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace rMind.Elements
{
    using Storage;
    /// <summary>
    /// Starage section
    /// </summary>
    public partial class rMindBaseController : IStorageObject
    {
        public virtual XElement Serialize()
        {           
            var controller = new XElement("controller");
            var attribure = new XAttribute("name", Name);
            controller.Add(attribure);

            controller.Add(ItemsNode());
            controller.Add(WiresNode());

            return controller;
        }

        protected virtual XElement ItemsNode()
        {
            var itemsNode = new XElement("items");
            foreach (var item in m_items)
            {
                var itemNode = item.Serialize();
                if (itemNode == null)
                    continue;

                itemsNode.Add(itemNode);
            }
           

            return itemsNode;
        }

        protected virtual XElement WiresNode()
        {
            var itemsNode = new XElement("wires");
            return itemsNode;
        }
    }
}

