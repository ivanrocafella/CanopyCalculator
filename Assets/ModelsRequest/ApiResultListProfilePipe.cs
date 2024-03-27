using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.ModelsRequest
{
    [Serializable]
    public class ApiResultListProfilePipe
    {
        public bool succeded { get; set; }
        //public ProfilePipeResponseModel[] Result { get; set; }
        //public IEnumerable<string> Errors { get; set; }
    }
}
