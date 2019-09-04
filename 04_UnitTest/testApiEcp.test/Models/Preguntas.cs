using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace testEcpApi.Models
{
    public class Preguntas
    {
        [Key]
        public Int32 Id { get; set; }
        public Int32 CategoriaId { get; set; }
        public string Pregunta { get; set; }
        public string Respuesta { get; set; }
        public string Activa { get; set; }

    }
}
