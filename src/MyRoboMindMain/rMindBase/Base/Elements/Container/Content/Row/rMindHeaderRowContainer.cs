﻿using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;


namespace rMind.Content
{
    using Elements;
    using rMind.Content.Row;
    using rMind.ColorContainer;
    using rMindBaseControls.Buttons;

    public partial class rMindHeaderRowContainer : rMindRowContainer
    {
        Border m_header_rect;

        
        protected HeaderButton m_expand_button;
        
        protected HeaderButton ExpandButton
        {
            get
            {
                if (m_expand_button == null)
                {
                    m_expand_button = new HeaderButton()
                    {
                        Content = new FontIcon
                        {
                            FontFamily = new FontFamily("Segoe MDL2 Assets"),
                            Glyph = "\uE96E",
                            FontSize = 10
                        },
                        Padding = new Thickness(8, 0, 8, 0),
                        BorderThickness = new Thickness(0, 0, 1, 0),
                        Background = rMindColors.GetInstance().GetSolidBrush(Colors.Transparent),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Left
                    };

                    Grid.SetColumnSpan(m_expand_button, 3);
                    m_expand_button.Click += ExpandButtonClick;
                    m_template.Children.Add(m_expand_button);
                }               
                return m_expand_button;
            }
        }

        protected bool m_can_expand = false;

        /// <summary>
        /// container can expand/collapse rows
        /// </summary>
        public bool CanExpand
        {
            get { return m_can_expand; }
            set { SetCanExpand(value); }
        }

        protected virtual void SetCanExpand(bool state)
        {
            if (state == m_can_expand)
                return;

            ExpandButton.Visibility = state ? Visibility.Visible : Visibility.Collapsed;
        }

        protected virtual void ExpandButtonClick(object sender, RoutedEventArgs args)
        {
            Expand();
        }

        protected void Expand()
        {
            if (m_expanded)
            {
                (m_expand_button.Content as FontIcon).Glyph = "\uE970";

                foreach (var row in m_rows)
                {
                    row.SetVisibility(false);
                }
                m_expanded = false;
            }
            else
            {
                (m_expand_button.Content as FontIcon).Glyph = "\uE96E";

                foreach (var row in m_rows)
                {
                    row.SetVisibility(true);
                }
                m_expanded = true;
            }

            Template.UpdateLayout();
            SetBorderRadius(m_border_radius);
            SetBorderThickness(m_border_thickness);
            SetPosition(Position);
        }

        protected HeaderButton m_edit_button;

        protected HeaderButton EditButton
        {
            get
            {
                if (m_edit_button == null)
                {
                    m_edit_button = new HeaderButton()
                    {
                        Content = new FontIcon
                        {
                            FontFamily = new FontFamily("Segoe MDL2 Assets"),
                            Glyph = "\uE115",
                            FontSize = 14
                        },
                        Padding = new Thickness(8, 0, 8, 0),
                        Background = rMindColors.GetInstance().GetSolidBrush(Colors.Transparent),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };

                    Grid.SetColumnSpan(m_edit_button, 3);
                    m_edit_button.Click += EditButtonClick;
                    m_template.Children.Add(m_edit_button);
                }
                
                return m_edit_button;
            }
        }

        protected bool m_can_edit = false;

        /// <summary>
        /// container can expand/collapse rows
        /// </summary>
        public bool CanEdit
        {
            get { return m_can_edit; }
            set { SetCanEdit(value); }
        }

        protected virtual void SetCanEdit(bool state)
        {
            if (state == m_can_edit)
                return;

            EditButton.Visibility = state ? Visibility.Visible : Visibility.Collapsed;
        }

        protected virtual void EditButtonClick(object sender, RoutedEventArgs args)
        {
            GetController()?.CanvasController.SetController(InnerController);
        }


        public struct rMindHeaderRowContainerTheme
        {
            public Color MainColor;
            public Color BorderColor;
            public Color HeaderColor;
        }

        public Color HeaderColor {
            get
            {
                return (m_header_rect.Background as SolidColorBrush).Color;
            }
            set
            {
                /*
                var shades = ColorForge.ColorHelper.GetColorShades(m_accent_color, 6);
                m_header_rect.Background = rMindColors.GetInstance().GetSolidBrush(shades[3]);
                m_header_rect.BorderBrush = rMindColors.GetInstance().GetSolidBrush(shades[2]);
                */
            }
        }

        TextBlock m_header_label;

        public string Header {
            get { return m_header_label.Text; }
            set { m_header_label.Text = value; }
        }


        public rMindHeaderRowContainer(rMindBaseController parent) : base(parent)
        {
            CanExpand = true;
            CanEdit = true;
        }

        public override void Init()
        {
            base.Init();
            var colors = rMindColors.GetInstance();

            m_header_rect = new Border()
            {
                IsHitTestVisible = false,
                Background = new SolidColorBrush(Colors.Black),
                MinHeight = 32
            };

            Grid.SetColumnSpan(m_header_rect, 3);           

            Template.Children.Add(m_header_rect);

            // Заголовок
            m_header_label = new TextBlock()
            {
                Text = "Header",
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily("Segoe UI"),
                Foreground = colors.GetSolidBrush(Colors.White),
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                IsHitTestVisible = false,
                Margin = new Thickness(36, 0, 36, 0)
            };
            
            Grid.SetColumnSpan(m_header_label, 3);

            Template.Children.Add(m_header_label);

            HeaderColor = Colors.IndianRed;
        }

        protected override int GetRowIndex(rMindRow row)
        {
            return base.GetRowIndex(row) + 1;
        }

        protected override int GetBaseRowSpan()
        {
            return base.GetBaseRowSpan() + 1;
        }

        protected override void SetBorderRadius(CornerRadius value)
        {
            base.SetBorderRadius(value);
            if (m_expanded)
                m_header_rect.CornerRadius = new CornerRadius(value.TopLeft, value.TopRight, 0, 0);                    
            else
                m_header_rect.CornerRadius = value;
        }

        protected override void SetAccentColor(Color color)
        {
            base.SetAccentColor(color);
            var shades = ColorForge.ColorHelper.GetColorShades(m_accent_color, 8);

            m_header_rect.Background = rMindColors.GetInstance().GetSolidBrush(shades[6]);            
            m_header_rect.BorderBrush = rMindColors.GetInstance().GetSolidBrush(shades[5]);
            m_header_label.Foreground = rMindColors.GetInstance().GetSolidBrush(shades[3]);

            EditButton.Foreground = rMindColors.GetInstance().GetSolidBrush(shades[2]);
            ExpandButton.Foreground = rMindColors.GetInstance().GetSolidBrush(shades[2]);
            ExpandButton.BorderBrush = rMindColors.GetInstance().GetSolidBrush(shades[5]);
        }

        protected override void SetBorderThickness(Thickness value)
        {
            base.SetBorderThickness(value);
            if (m_expanded)
            {
                m_header_rect.BorderThickness = new Thickness(
                    value.Left, value.Top, value.Right, 0
                );
            }
            else
                m_header_rect.BorderThickness = value;
        }
    }
}
