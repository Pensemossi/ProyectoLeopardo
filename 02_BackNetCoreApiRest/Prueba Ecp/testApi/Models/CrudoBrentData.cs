using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class CrudoBrentData
    {
        public Decimal price { get; set; }
        public string formatted { get; set; }
        public string currency { get; set; }

        public string code { get; set; }
        public DateTime created_at { get; set; }

    }
}


