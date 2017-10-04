using System.Linq;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using System.Collections.Generic;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace rMind.CanvasEx
{
    using rMind.Storage;
    using Elements;

    public class rMindCanvasController
    {
        rMindBaseController m_root_controller;
        rMindBaseController m_current_controller;

        public rMindBaseController CurrentController {  get { return m_current_controller; } }       

        Canvas m_canvas;
        ScrollViewer m_scroll;

        ObservableCollection<rMindBaseController> m_bread_crumbs;

        public rMindCanvasController(Canvas canvas, ScrollViewer scroll)
        {
            m_canvas = canvas;
            m_scroll = scroll;

            m_bread_crumbs = new ObservableCollection<rMindBaseController>();
        }

        public void SetController(rMindBaseController controller)
        {
            if (m_root_controller == null)
                m_root_controller = controller;


            if (m_bread_crumbs.Contains(controller))
            { // Переход по хлебным крошкам.
                var idx = m_bread_crumbs.IndexOf(controller);

                m_current_controller?.Unsubscribe();
                m_current_controller = controller;
                m_current_controller.Subscribe(m_canvas, m_scroll);

                var remove = m_bread_crumbs.Where(x => m_bread_crumbs.IndexOf(x) > idx).ToList();
                foreach (var item in remove)
                    m_bread_crumbs.Remove(item); 

                return;
            }

            m_current_controller?.Unsubscribe();
            if (controller == null)
                return;

            m_current_controller = controller;
            m_current_controller.Subscribe(m_canvas, m_scroll);
            m_bread_crumbs.Add(m_current_controller);
        }

        public void Back()
        {
            if (m_bread_crumbs.Count < 1)
                return;

            m_current_controller?.Unsubscribe();
            m_bread_crumbs.Remove(m_current_controller);

            m_current_controller = m_bread_crumbs.LastOrDefault();
            m_current_controller?.Subscribe(m_canvas, m_scroll);
        }

        public XElement Serialize()
        {
            if (m_root_controller == null)
                return null;

            var node = m_root_controller.Serialize();
            return null;                   
        }

        public XDocument GetXML()
        {
            if (m_root_controller == null)
                return null;

            var doc = new XDocument();
            var node = m_root_controller.Serialize();
            doc.AddFirst(node);
            return doc;
        }

        public void LoadFromXML(XDocument doc)
        {
            var root = doc.Elements().FirstOrDefault();

            if (m_root_controller == null)
                return;

            m_root_controller.Deserialize(root);

        }

        public ObservableCollection<rMindBaseController> BreadCrumbs { get { return m_bread_crumbs; } }
    }
}
