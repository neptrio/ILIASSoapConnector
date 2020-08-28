using System;
using System.Collections.Generic;
using System.Text;

namespace ILIASSoapConnector.Models
{
    public class IliasUser
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public bool Active { get; set; }
    }
}
