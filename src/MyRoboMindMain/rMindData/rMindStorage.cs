using System.IO.IsolatedStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Storage
{
    /// <summary>
    /// Контроллер для хранения данных 
    /// </summary>
    class rMindStorage
    {
        static rMindStorage m_instance;
        static object sync = new object();

        public static rMindStorage GetInstance()
        {
            if (m_instance == null)
            {
                lock(sync)
                {
                    if (m_instance == null)
                    {
                        m_instance = new rMindStorage();
                    }
                }
            }
            return m_instance;
        }

        public rMindStorage()
        {

        }
    }
}
