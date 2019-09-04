using System.ComponentModel.DataAnnotations;

namespace testEcpApi.Models
{
    public class Usuarios
    {
        [Key]
        public string Registro { get; set; }
        public string Nombres { get; set; }
        public string Clave { get; set; }
        public string Token { get; set; }
        public string EsAdministrador { get; set; }

    }
}
