using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class AccionECPdto
    {
        public Decimal latestPrice { get; set; }
        public DateTime latestUpdate { get; set; }

    }
}


