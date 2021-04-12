using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Serializable]
    public class Importer
    {
        public int Id { get; set; }
        public string PointOfSale { get; set; }
        public string Product { get; set; }
        public string Date { get; set; }
        public string Stock { get; set; }
    }
}
