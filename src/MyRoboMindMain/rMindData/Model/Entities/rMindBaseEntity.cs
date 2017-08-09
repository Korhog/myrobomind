using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMindData.Model.Entities
{
    public class rMindBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SemanticName { get; set; }
    }
}
