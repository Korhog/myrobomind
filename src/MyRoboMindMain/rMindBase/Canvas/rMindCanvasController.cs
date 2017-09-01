using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

namespace rMind.CanvasEx
{
    using Elements;

    public class rMindCanvasController
    {
        rMindBaseController m_current_controller;
        Canvas m_canvas;
        ScrollViewer m_scroll;
        ScaleTransform m_scale;

        public rMindCanvasController(Canvas canvas, ScrollViewer scroll, ScaleTransform scale)
        {
            m_canvas = canvas;
            m_scroll = scroll;
            m_scale = scale;
        }

        public void SetController(rMindBaseController controller)
        {
            m_current_controller?.Unsubscribe();
            m_current_controller = controller;
            m_current_controller?.Subscribe(m_canvas, m_scroll, m_scale);
        }
    }
}
