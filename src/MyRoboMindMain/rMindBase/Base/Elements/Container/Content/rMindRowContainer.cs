using System;
using System.Collections.Generic;
using System.Linq;

using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml.Controls;
using System.Threading.Tasks;

namespace rMind.Content
{
    using Row;

    using Elements;

    /// <summary>
    /// Контейнер с возможностью добавлять строки с данными
    /// </summary>
    public class rMindRowContainer : rMindBaseElement
    {
        protected List<rMindRow> m_rows;

        public rMindRowContainer(rMindBaseController parent) : base(parent)
        {
            Template.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(40) });
            Template.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            Grid.SetColumnSpan(m_base, 3);

            m_rows = new List<rMindRow>(); 
        }

        public virtual rMindRow AddRow()
        {
            return AddRow(new rMindRow());
        }

        public virtual rMindRow AddRow(rMindRow row)
        {
            m_rows.Add(row);
            // Данная секция добавлена чисто для тестирования
            if (m_rows.Count > 0)
            {
                // Если это не первая строка, то надо добавить строку в шаблон
                Template.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            }

            //
            var idx_row = m_rows.IndexOf(row);

            CreateNode().SetCell(0, idx_row);
            CreateNode().SetCell(2, idx_row);

            var rect = new Rectangle()
            {
                Height = 20,
                Margin = new Thickness(2),
                Fill = new SolidColorBrush(Colors.CadetBlue),
                IsHitTestVisible = false
            };

            Grid.SetColumn(rect, 1);
            Grid.SetRow(rect, idx_row);
            Template.Children.Add(rect);

            Grid.SetRowSpan(m_base, m_rows.Count);

            return row;
        }
    }
}
