using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Models
{
    [Serializable]
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public User()
        {
        }

        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
