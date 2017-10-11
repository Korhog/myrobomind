using System.Xml.Linq;

using Windows.UI.Xaml.Controls;

namespace rMind.Project
{
    using CanvasEx;
   

    public class rMindProject
    {
        static rMindProject m_instance;
        static object sync = new object();

        rMindCanvasController m_scheme_controller;
        rMindCanvasController m_device_controller;

        public rMindCanvasController SchemeController { get { return m_scheme_controller; } }
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
            m_device_controller = new rMindCanvasController(canvas, scroll);
            Elements.rMindBaseController controller = new Elements.rMindBaseController(m_device_controller);
            m_device_controller.SetController(controller);
        }

        async void Setup(rMindCanvasController device, rMindCanvasController scheme)
        {
            m_scheme_controller = scheme;
            m_device_controller = device;

            var storage = Storage.rMindStorage.GetInstance();
            var doc = await storage.LoadTmpData();

            m_device_controller.LoadFromXML(doc);
        }

        public static rMindProject Create(rMindCanvasController device, rMindCanvasController scheme)
        {
            var project = GetInstance();
            project.Setup(device, scheme);
            return project;
        }

        /// <summary>
        /// Save current project state in temp folder
        /// </summary>
        public void  SaveState()
        {
            if (m_device_controller == null)
                return;

            var data = m_device_controller.GetXML();
            var storage = rMind.Storage.rMindStorage.GetInstance();
            storage.SaveTmpData(data);
        }

        /// <summary>
        /// restore project state from temp folder
        /// </summary>
        public async void RestoreState()
        {
            var storage = rMind.Storage.rMindStorage.GetInstance();
            var doc = await storage.LoadTmpData();
            DeviceController.LoadFromXML(doc);
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
