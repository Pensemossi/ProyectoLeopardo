using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class Categorias
    {
        [Key]
        public Int32 Id { get; set; }
        public string Nombre { get; set; }
    }
}
