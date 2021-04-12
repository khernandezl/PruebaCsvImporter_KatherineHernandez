using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Constants
    {
        private readonly IConfiguration configuration;
        public Constants(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public static readonly string urlCSV = "https://storage10082020.blob.core.windows.net/y9ne9ilzmfld/Stock.CSV";
        public static readonly string urlCSV2 = "C:/Users/khern/Downloads/PruebaCsv.csv";
    }
}
