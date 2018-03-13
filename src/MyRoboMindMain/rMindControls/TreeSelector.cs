using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace rMind.Controls
{
    using Entities;

    public delegate void OnSelectorItemDelegate(object selected);

    public sealed class TreeSelector : Control
    {
        public event OnSelectorItemDelegate ItemSelect;

        Border overlay;
        Button menuButton;
        FontIcon menuIcon;
        ListView items;

        Brush overlayBrush;

        TreeSelectorItem root;

        public TreeSelector()
        {
            DefaultStyleKey = typeof(TreeSelector);
            overlayBrush = new SolidColorBrush(Colors.Transparent);
        }

        public void SetRoot(TreeSelectorItem rootItem)
        {
            root = rootItem;
            DataContext = root;
        }

        void OpenTreeSelector()
        {
            DataContext = root;

            overlay.Background = overlayBrush;
            items.Visibility = Visibility.Visible;
            menuIcon.Glyph = "\uE0A6";
        }

        void CloseTreeSelector()
        {
            items.Visibility = Visibility.Collapsed;
            overlay.Background = null;
            menuIcon.Glyph = "\uE109";
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            items = GetTemplateChild("PART_Items") as ListView;
            items.ItemClick += (s, e) =>
            {
                var item = e.ClickedItem as TreeSelectorItem;
                if (item == null)
                    return;

                if (item.Folder)
                {
                    DataContext = item;
                }
                else
                {
                    CloseTreeSelector();
                    ItemSelect?.Invoke(item);
                }
            };

            overlay = GetTemplateChild("PART_Overlay") as Border;
            overlay.PointerPressed += (s, e) =>
            {
                CloseTreeSelector();
            };

            menuButton = GetTemplateChild("PART_MenuButton") as Button;
            menuIcon = GetTemplateChild("PART_MenuIcon") as FontIcon;
            menuButton.Click += (s, e) => {
                if (items.Visibility == Visibility.Collapsed)
                {
                    OpenTreeSelector();
                }
                else
                {
                    var item = DataContext as TreeSelectorItem;
                    if (item == null || item.Parent == null)
                        return;

                    DataContext = item.Parent;
                }
            };
        }
    }
}
