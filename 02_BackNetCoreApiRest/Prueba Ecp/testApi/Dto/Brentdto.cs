using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class BrentDto
    {
        public Decimal Price { get; set; }
        public DateTime created_at { get; set; }

    }
}


