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
    public sealed class IconListBoxItem : ListBoxItem
    {
        public IconListBoxItem()
        {
            DefaultStyleKey = typeof(IconListBoxItem);
        }

        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
            "Caption",
            typeof(string),
            typeof(IconListBoxItem),
            new PropertyMetadata(null)
        );


        public string Glyph
        {
            get { return (string)GetValue(GlyphProperty); }
            set { SetValue(GlyphProperty, value); }
        }

        public static readonly DependencyProperty GlyphProperty = DependencyProperty.Register(
            "Glyph",
            typeof(string),
            typeof(IconListBoxItem),
            new PropertyMetadata(null)
        );

        public double GlyphWidth
        {
            get { return (double)GetValue(GlyphWidthProperty); }
            set { SetValue(GlyphWidthProperty, value); }
        }

        public static readonly DependencyProperty GlyphWidthProperty = DependencyProperty.Register(
            "GlyphWidth",
            typeof(double),
            typeof(IconListBoxItem),
            new PropertyMetadata((double)48)
        );
    }
}
