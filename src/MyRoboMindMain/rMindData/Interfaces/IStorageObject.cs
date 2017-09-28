using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMind.Storage
{
    public interface IStorageObject
    {
        object Serialize();
        // object Deserialize(object json);
    }
}
