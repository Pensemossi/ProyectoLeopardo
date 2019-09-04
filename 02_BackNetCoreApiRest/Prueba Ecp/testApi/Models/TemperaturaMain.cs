using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace testEcpApi.Models
{
    public class TemperaturaMain
    {
        public Int16 temp { get; set; }
        public Int32 pressure { get; set; }
        public Int16 humidity { get; set; }
        public Int16 temp_min { get; set; }
        public Int16 temp_max { get; set; }
    }
}