using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMindData.Model.Entities.Types
{
    public enum BaseDataTypes
    {
        Number,
        Text
    }

    public class rMindDataType
    {
        public bool IsReference { get; set; } //Ссылочный тип
    }

    // Сущность для хранения в БД
    public class rMindDataTypeEntity : rMindBaseEntity
    {        
        public BaseDataTypes BaseType { get; set; }
    }
}
