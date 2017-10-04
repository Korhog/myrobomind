using System;
using System.Xml.Linq;

using Windows.Storage;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Storage
{
    public class rMindStorage
    {
        string m_tmp_file_name;

        static rMindStorage m_instance;
        static object sync = new Object();

        rMindStorage()
        {
            m_tmp_file_name = "tmp.xml";
        }

        public static rMindStorage GetInstance()
        {
            if(m_instance == null)
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

        /// <summary> Save temp project to local storage </summary>
        /// <param name="xml"></param>
        public async void SaveTmpData(XDocument xml)
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var tmpFile = await local.CreateFileAsync(m_tmp_file_name, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(tmpFile, xml.ToString());
        }

        /// <summary> Load temp project from local storage </summary>
        public async Task<XDocument> LoadTmpData()
        {
            StorageFolder local = ApplicationData.Current.LocalFolder;
            var tmpFile = await local.GetFileAsync(m_tmp_file_name);
            if (tmpFile == null)
                return null;

            string text = await FileIO.ReadTextAsync(tmpFile);

            XDocument doc = XDocument.Parse(text);
            return doc;
        }
    }
}
