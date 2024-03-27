using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ModelsRequest
{
    public class ApiResult<T>
    {
        public bool Succeded { get; set; }
        public T Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
