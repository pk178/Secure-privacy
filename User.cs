using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Secure_privacy
{
    public class User
    {
        public string loginName { set; get; }
        public string password { set; get; }
        public string fullName { set; get; }
        public string department { set; get; }
        public DateTime createTime { set; get; }
    }
}
