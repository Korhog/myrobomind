using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Driver.Entities
{
    /// <summary>
    /// Base data type. 
    /// </summary>
    public enum BaseDataType
    {
        /// <summary> None type </summary>
        None,
        /// <summary> Numeric types [int, float, ... ] </summary>
        Numeric,
        /// <summary> Text types [char, char[], ... ] </summary>
        Text
    }

    /// <summary> Data type base </summary>
    public class DataTypeBase
    {
        public BaseDataType BaseDataType { get; set; }        
    }
}
