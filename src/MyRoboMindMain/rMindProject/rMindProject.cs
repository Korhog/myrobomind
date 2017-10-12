using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace rMind.Project
{
    using CanvasEx;
   

    public class rMindProject
    {
        static rMindProject m_instance;
        static object sync = new object();

        rMindCanvasController m_logic_controller;
        rMindCanvasController m_device_controller;

        public rMindCanvasController LogicController { get { return m_logic_controller; } }
        public rMindCanvasController DeviceController { get { return m_device_controller; } }

        public static rMindProject GetInstance()
        {
            if (m_instance == null)
            {
                lock (sync)
                {
                    if (m_instance == null)
                    {
                        m_instance = new rMindProject();
                    }
                }
            }

            return m_instance;
        }

        public void SetupDevice(Canvas canvas, ScrollViewer scroll)
        {
            m_device_controller = new rMindCanvasController(canvas, scroll) { NodeName = "device" };
            Elements.rMindBaseController controller = new rMindDeviceController(m_device_controller);
            m_device_controller.SetController(controller);
        }

        public void SetupLogic(Canvas canvas, ScrollViewer scroll)
        {
            m_logic_controller = new rMindCanvasController(canvas, scroll) { NodeName = "logic" };
            Elements.rMindBaseController controller = new Elements.rMindBaseController(m_logic_controller);
            m_logic_controller.SetController(controller);
        }

        /// <summary>
        /// Save current project state in temp folder
        /// </summary>
        public async Task SaveState()
        {
            if (m_device_controller == null)
                return;

            var doc = new XDocument();
            var projectNode = new XElement("project");

            projectNode.Add(m_device_controller.GetXML());
            projectNode.Add(m_logic_controller.GetXML());

            doc.Add(projectNode);

            var storage = Storage.rMindStorage.GetInstance();
            await storage.SaveTmpData(doc);            
        }

        /// <summary>
        /// restore project state from temp folder
        /// </summary>
        public async Task RestoreState()
        {
            var storage = rMind.Storage.rMindStorage.GetInstance();
            var doc = await storage.LoadTmpData();
            var root = doc?.Root;

            if (root != null)
            {
                DeviceController.LoadFromXML(root.Elements("device").FirstOrDefault());
                LogicController.LoadFromXML(root.Elements("logic").FirstOrDefault());
            }

            DeviceController.Draw();
            LogicController.Draw();
        }

        /// <summary>
        /// Reset project state (new project)
        /// </summary>
        public void Reset()
        {
            DeviceController?.Reset();
        }
    }
}
