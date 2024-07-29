using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class TypeDocuments
    {
        public Guid ID { get; set; }
        public string Type { get; set; }
        public string Abbreviation { get; set; }
        public int State { get; set; }

    }
}
