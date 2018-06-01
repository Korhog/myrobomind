using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;

namespace rMind.IoT.NodeEngine
{
    public class ColorMapper
    {
        public static Color Get(Type t)
        {
            if (t.Equals(typeof(bool)))
            {
                return Colors.Red;
            }        

            return Colors.SkyBlue;
        }
    }
}
