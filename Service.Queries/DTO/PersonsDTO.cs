using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.DTO
{
    public class PersonsDTO
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int NumberDocument { get; set; }
        public string Email { get; set; }

        public Guid TypeDocument { get; set; }
        public DateTime AddAt { get; set; }
        public string NumberDocumentTypeDocument { get; set; }
        public string FullName { get; set; }
    }
}
