using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace rMind.Controls
{
    using rMind.Driver.Entities;
    using Windows.ApplicationModel.DataTransfer;
    using Windows.UI.Xaml.Media.Animation;

    public delegate void OnCreateButtonDelegate(ITreeItem folder);

    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate ItemTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is TreeFolder)
                return DefaultTemplate;

            if (item is Driver.Driver)
                return ItemTemplate;

            return DefaultTemplate;
        }
    }

    public sealed class Tree : Control
    {
        ITreeItem root;
        ITreeItem buffer;

        ListView items;
        ListView subItems;
        Button backButton;
        TranslateTransform slideTransform;
        TranslateTransform slideSubTransform;



        Border bucket;

        public event OnCreateButtonDelegate OnCreateButton;

        public Tree()
        {
            this.DefaultStyleKey = typeof(Tree);            
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            items = GetTemplateChild("PART_Items") as ListView;
            slideTransform = GetTemplateChild("PART_SlideTransform") as TranslateTransform;
            slideSubTransform = GetTemplateChild("PART_SlideSubTransform") as TranslateTransform;

            bucket = GetTemplateChild("PART_Bucked") as Border;
            bucket.DragOver += (s, e) =>
            {
                e.AcceptedOperation = DataPackageOperation.Move;               
            };

            bucket.Drop += async (s, e) =>
            {
                if (buffer == null)
                    return;

                TreeFolder folder = this.DataContext as TreeFolder;
                if (folder == null)
                    return;

                folder.Remove(buffer);
            };


            subItems = GetTemplateChild("PART_SlideItems") as ListView;

            var adder = GetTemplateChild("PART_AddButton") as Button; 
            adder.Click += (s, e) =>
            {
                ITreeItem folder = (DataContext as ITreeItem) ?? root;
                OnCreateButton?.Invoke(folder);
            };

            items.ItemClick += (s, e) =>
            {                
                var f = e.ClickedItem as TreeFolder;
                if (f != null)
                {
                    backButton.Visibility = Visibility.Visible;
                    subItems.ItemsSource = f.Parent.Children;
                    DataContext = f;
                    Slide(true);
                    return;
                }               
            };

            items.DataContextChanged += (s, e) => {
                var list = (s as ListView).Items;
                var a = e;
            };

            items.DragItemsStarting += (s, e) =>
            {
                e.Data.RequestedOperation = DataPackageOperation.Move;
                buffer = e.Items.First() as ITreeItem;
            };

            backButton = GetTemplateChild("PART_BackButton") as Button;
            backButton.Click += (s, e) =>
            {
                Button sender = s as Button;
                ITreeItem folder = DataContext as ITreeItem;
                if (folder == null)
                {
                    return;
                }

                subItems.ItemsSource = folder.Children;
                if (folder.Parent == root)                
                    sender.Visibility = Visibility.Collapsed;

                DataContext = folder.Parent;
                Slide(false);
            };
        }

        void Slide(bool forward)
        {
            Storyboard storyboard = new Storyboard();
            DoubleAnimation slideAnimation = new DoubleAnimation();

            if (forward)
            {
                slideAnimation.From = 250;
                slideAnimation.To = 0;
            }
            else
            {
                slideAnimation.From = -250;
                slideAnimation.To = 0;
            }
            slideAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            Storyboard.SetTarget(slideAnimation, slideTransform);
            Storyboard.SetTargetProperty(slideAnimation, "X");

            storyboard.Children.Add(slideAnimation);

            slideAnimation = new DoubleAnimation();

            if (forward)
            {
                slideAnimation.From = 0;
                slideAnimation.To = -250;
            }
            else
            {
                slideAnimation.From = 0;
                slideAnimation.To = 250;
            }
            slideAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(250));

            Storyboard.SetTarget(slideAnimation, slideSubTransform);
            Storyboard.SetTargetProperty(slideAnimation, "X");

            storyboard.Children.Add(slideAnimation);

            storyboard.Begin();
        }

        public void SetRoot(ITreeItem target)
        {
            root = target;
            DataContext = target;
        }
    }
}
