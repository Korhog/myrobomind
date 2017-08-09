using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rMindData.Model.Entities.Class
{
    public class rMindVariable : ICodeGeneratable
    {
        public GenerationResult GetSourceCode(int level)
        {   
            return new GenerationResult
            {
                Result = GenerationResultCode.Success,
                SourceCode = null
            };
        }
    }

    public class rMindVariableEntity : rMindBaseEntity
    {
    }
}
