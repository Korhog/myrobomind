using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMindData.Model.Entities.Class
{
    /// <summary>
    /// Класс, это некоторая сущность, которая умеет взаимодействовать
    /// с оборудованием. У нег есть методы, и "интерфейсы" (публичные методы)
    /// </summary>
    public class rMindClass
    {
    }

    public class rMindClassEntity : rMindBaseEntity
    {
        public bool IsStatic { get; set; }
    }
}
