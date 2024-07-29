using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Users
    {
        public Guid ID { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public DateTime AddAt { get; set; }    
    }
}
