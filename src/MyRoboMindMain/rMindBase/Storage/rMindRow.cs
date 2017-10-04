using System.Xml.Linq;

namespace rMind.Content.Row
{
    using Storage;

    public partial class rMindRow : IStorageObject
    {
        #region Serialize
        public virtual XElement Serialize()
        {
            var node = new XElement("row");
            return node;
        }        
        #endregion

        #region Deserialize
        public virtual void Deserialize(XElement node)
        {

        }

        #endregion
    }
}
