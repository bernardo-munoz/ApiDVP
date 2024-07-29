using MediatR;
using Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.EventHandler.Commands
{
    public class UsersCreateCommand : IRequest<RequestResult>
    {
        public string User { get; set; }
        public string Pass { get; set; }
    }
}
