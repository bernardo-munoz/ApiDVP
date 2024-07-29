using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.DTO
{
    public class LoginDTO
    {
        public required string User { get; set; }
        public required string Pass { get; set; }
    }
}
