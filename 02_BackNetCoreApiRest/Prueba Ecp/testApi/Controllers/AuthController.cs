using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using testEcpApi.Models;
using testEcpApi.Service;
using testEcpApi.Data;
using System.Linq;

namespace testEcpApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(testContext context)
        {
            _service = new AuthService(context);
            if (context.DbUsuarios.Count() == 0)
            {
                // Para pruebas BD en Memoria
                DataTestMemory(context);


            }


        }
        //********************* INICIO API REST ****************************************************
        //POST /api/authenticate/token
        /// <summary>
        /// Recibe usuario y clave en la cabecera del mensaje y si es valida la autenticación
        /// retorna el atributo token con el valor generado. Valor vacio en caso contrario.
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult> Token()
        {
            try
            {
                NetworkCredential credentials = GetCredentials();
                string token = await _service.Authenticate(credentials);

                if (token == "")
                {
                    return Unauthorized();
                }

                Usuarios usuario= await _service.GetUsuarioById(credentials.UserName);

                var userToken = new Usuarios { Token = token, EsAdministrador = usuario.EsAdministrador, Registro= credentials.UserName, Nombres= usuario.Nombres };

                return Ok(userToken);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Unauthorized.ToString(), ex.Message);
            }
        }

        //********************* FIN API REST ****************************************************

        /// <summary>
        /// Captura usuario y contraseña del header del mensaje encripatado
        /// </summary>
        private NetworkCredential GetCredentials()
        {
            string authHeader = HttpContext.Request.Headers["Authorization"];

            if (authHeader == null || !authHeader.StartsWith("Basic"))
                throw new Exception("Authorization header esta vacía o no es basic.");

            string usernamePassword = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(authHeader.Substring("Basic ".Length).Trim()));

            int seperatorIndex = usernamePassword.IndexOf(':');

            return new NetworkCredential(usernamePassword.Substring(0, seperatorIndex), usernamePassword.Substring(seperatorIndex + 1));

        }
        /// <summary>
        /// Valida usuario y clave, retorna nuevo token en el caso que sean validos
        /// </summary>
        private async Task<string> Authenticate(NetworkCredential credentials)
        {
            //Leer de la BD el usaurio que coincida con el username
            return await _service.Authenticate(credentials);
        }

        //Funciòn solo para test, en BD InMemory
        private void DataTestMemory(testContext context)
        {
            context.DbUsuarios.Add(new Usuarios { Registro = "C100020", Nombres = "Kristian Espitia", Clave = "12345", EsAdministrador = "N" });
            context.DbUsuarios.Add(new Usuarios { Registro = "C100021", Nombres = "Juan Cardona", Clave = "12345", EsAdministrador = "S" });

            context.DbCategorias.Add(new Categorias { Nombre = "HSE" });
            context.DbCategorias.Add(new Categorias { Nombre = "Salud" });

            context.DbPreguntas.Add(new Preguntas { CategoriaId = 1, Activa = "S", Pregunta = "Si tengo contrato de trabajo con Ecopetrol, ¿a partir de qué momento puedo empezar a hacer uso de los servicios de Salud?", Respuesta = "Desde el día en que ingresa a la compañía con un contrato de trabajado firmado con Ecopetrol se convierte en beneficiario del Plan de Salud y podrá hacer uso de los servicios presentando la cédula" });
            context.DbPreguntas.Add(new Preguntas { CategoriaId = 1, Activa = "S", Pregunta = "¿Cómo inscribo a mis familiares y beneficiarios?", Respuesta = "Usted puede realizar la inscripción o ratificación de sus familiares en las oficinas de personal diligenciando el formato publicado en IRIS y aportando la documentación exigida para cada familiar. Si la documentación es correcta y está completa los nuevos beneficiarios podrán disfrutar de todos los beneficios" });
            context.DbPreguntas.Add(new Preguntas { CategoriaId = 2, Activa = "S", Pregunta = "¿Cómo escojo a mi médico de cabecera por primera vez?", Respuesta = "Debe tramitar la selección de su profesional de cabecera: médico general, pediatra, odontólogo general u odontopediatra, según sea el casos ante las dependencias de Servicios de Salud o realizar la solicitud escribiendo al correo oficinavirtualdesalud@ecopetrol.com.co" });

            context.SaveChanges();
        }
    }
}