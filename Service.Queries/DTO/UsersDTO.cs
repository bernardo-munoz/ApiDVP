using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.DTO
{
    public class UsersDTO
    {
        public Guid ID { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }
        public DateTime AddAt { get; set; }
    }
}
