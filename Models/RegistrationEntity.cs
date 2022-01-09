using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class RegistrationEntity
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public EmailGroup emailGroup { get; set; }
    }
}
