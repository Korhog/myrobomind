using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Driver
{
    /// <summary> Arguments of data base state changes </summary>
    public class DBStateChangedHandlerArgs
    {

    }

    public delegate void DBStateChangedHandler(rMindDataBase sender, DBStateChangedHandlerArgs args);

    public class rMindDataBase
    {
        public event DBStateChangedHandler StateChanged;

        private static rMindDataBase instance;
        private static object sync = new object();

        public static rMindDataBase GetInstance()
        {
            if (instance == null)
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new rMindDataBase();
                    }
                }
            }
            return instance;
        }

        public void Test()
        {
            StateChanged?.Invoke(this, new DBStateChangedHandlerArgs());
        }
    }
}
