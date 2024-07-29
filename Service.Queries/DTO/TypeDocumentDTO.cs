using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries.DTO
{
    public class TypeDocumentDTO
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public int State { get; set; }
    }
}
