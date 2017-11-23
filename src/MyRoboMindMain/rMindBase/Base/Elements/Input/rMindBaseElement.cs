using Windows.UI.Xaml.Input;

namespace rMind.Elements
{
    using Draw;
    using Types;
    using Storage;
    using Input;

    /// <summary>
    /// Base controller element 
    /// </summary>   
    public partial class rMindBaseElement : rMindBaseItem, IDrawContainer, IStorageObject, IInteractElement
    {
        float zoom = 1;

        public void OnManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            // не взаимодействуем с заблокированным объектом
            if (Locked)
                return;

            e.Handled = true;
        }

        public void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // не взаимодействуем с заблокированным объектом
            if (Locked)
                return;

            e.Handled = true;
            Translate(new Vector2(e.Delta.Translation.X, e.Delta.Translation.Y) / zoom);
        }

        public void OnManipulationStarting(object sender, ManipulationStartingRoutedEventArgs e)
        {
            // не взаимодействуем с заблокированным объектом
            if (Locked)
                return;

            e.Handled = true; 

            zoom = GetController()?.CanvasController.Zoom ?? 1;
            if (zoom == 0)
                zoom = 1;
        }

        public void SubscribeInput()
        {
            m_base.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY;

            m_base.ManipulationStarting += OnManipulationStarting;
            m_base.ManipulationCompleted += OnManipulationCompleted;
            m_base.ManipulationDelta += OnManipulationDelta;   
        }

        public void UnsubscribeInput()
        {
            m_base.ManipulationStarting -= OnManipulationStarting;
            m_base.ManipulationCompleted -= OnManipulationCompleted;
            m_base.ManipulationDelta -= OnManipulationDelta;
        }
    }
}
