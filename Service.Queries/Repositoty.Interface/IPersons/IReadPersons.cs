using Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.Repositoty.Interface.IPersons
{
    public interface IReadPersons
    {
        public RequestResult GetAllPersons();
        public RequestResult GetPersonByNumberDocument(int numberDocument);
    }
}
