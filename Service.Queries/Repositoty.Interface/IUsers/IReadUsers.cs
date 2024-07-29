using Response;
using Service.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.Repositoty.Interface.IUsers
{
    public interface IReadUsers
    {
        public RequestResult Login(LoginDTO LoginDTO);
        public RequestResult GetAllUsers();
        RequestResult GetUsersByUser(string user);
    }
}
