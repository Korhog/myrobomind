using System.Linq;
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
        public object Serialize()
        {
            var items = m_items.Select(x => new { ids = x.IDS }).ToArray();
            var wires = m_wire_list.Select(x => new { code = "wire" }).ToArray();
            return new
            {
                name = Name,
                items = items,
                wires = wires
            };
        }
    }
}

