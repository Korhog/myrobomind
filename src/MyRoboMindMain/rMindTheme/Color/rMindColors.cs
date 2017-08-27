using System;
using Windows.UI;
using Windows.UI.Xaml.Media;
    
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.ColorContainer
{
    public class rMindColors
    {
        Dictionary<Windows.UI.Color, SolidColorBrush> m_solid_brushes;


        private static rMindColors m_instance;
        static object sync = new Object();

        rMindColors()
        {
            m_solid_brushes = new Dictionary<Windows.UI.Color, SolidColorBrush>();
        }

        public static rMindColors GetInstance()
        {
            if (m_instance == null)
            {
                lock (sync)
                {
                    if (m_instance == null)
                    {
                        m_instance = new rMindColors();
                    }
                }
            }

            return m_instance;
        }   
    }
}
