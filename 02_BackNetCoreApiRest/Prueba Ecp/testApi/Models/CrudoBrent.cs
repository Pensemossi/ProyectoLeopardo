using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class CrudoBrent
    {
        public string status { get; set; }
        public CrudoBrentData data { get; set; }

    }
}


