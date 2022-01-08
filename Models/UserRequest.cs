using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class UserRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public EmailGroup emailGroup { get; set; }
    }
}
