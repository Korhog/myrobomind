using System;
using System.Linq;
using System.Collections.Generic;

namespace rMind.Driver.Entities
{
    /// <summary> Driver types </summary>
    public enum DriverType
    {
        /// <summary> Library (Math, ...) </summary>
        Lib,
        /// <summary> Подключаемое устройство </summary>
        ExternalDevice
    }

    /// <summary> External device: Button, Led, ... </summary>
    public class Driver
    {
        public Driver()
        {
            Pins = new List<Pin>();
            Methods = new List<Method>();
        }

        /// <summary> Driver made by user </summary>
        public bool Custom { get; set; } = false;

        /// <summary> Driver name </summary>
        public string Name { get; set; }

        /// <summary> Driver type </summary>
        public DriverType Type { get; set; }

        /// <summary> Device pins </summary>
        public List<Pin> Pins { get; set; }

        /// <summary> Device methods 
        /// 
        /// </summary>
        public List<Method> Methods { get; set; }

        //
        public List<Method> Setters { get { return Methods.Where(m => m.DataType != null).ToList(); } }
        public List<Method> Getters { get { return Methods.Where(m => m.DataType == null).ToList(); } }
    }
}
