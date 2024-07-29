using Domain;
using Persistence.Database;
using Service.Queries.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.BO
{
    public class UsersBO
    {
        private readonly ApplicationDbContext _context;
        public UsersBO(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<UsersDTO> GetAllUsers()
        {
            //Se hace un IQueryable para aplanar la consulta lo mas posible
            IQueryable<Users> listusers = _context.Users
                .Select(x => new Users
                {
                    ID = x.ID,
                    User = x.User,
                    Pass = x.Pass,
                    AddAt = x.AddAt
                });

            //Con el Tolist nos traemos los datos a memoria y hacemos posprocesamiento...
            return listusers.Select(x => new UsersDTO
            {
                ID = x.ID,
                User = x.User,
                Pass = x.Pass,
                AddAt = x.AddAt
            }).ToList();

        }

        public UsersDTO? GetUsersByUser(string user)
        {
            //Se hace un IQueryable para aplanar la consulta lo mas posible
            IQueryable<Users> userDTO = _context.Users.Where(x => x.User == user)
                .Select(x => new Users
                {
                    ID = x.ID,
                    User = x.User,
                    Pass = x.Pass,
                    AddAt = x.AddAt
                });

           
            return userDTO.Select(x => new UsersDTO
            {
                ID = x.ID,
                Pass = x.Pass,
                User = x.User,
                AddAt = x.AddAt
            }).FirstOrDefault();
        }
    }
}
