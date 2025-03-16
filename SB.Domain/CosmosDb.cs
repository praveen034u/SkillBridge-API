using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Domain
{
    public class CosmosDb
    {
        public string AccountEndpoint { get; set; }
        public string AccountKey { get; set; }
        public string ContainerName { get; set; }
    }
}
