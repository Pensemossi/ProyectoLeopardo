using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class AccionECP
    {
        public Decimal latestPrice { get; set; }
        public double latestUpdate { get; set; }

    }
}


