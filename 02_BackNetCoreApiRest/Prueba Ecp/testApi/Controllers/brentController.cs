using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using testEcpApi.Models;
using testEcpApi.Service;
using testEcpApi.Data;


namespace testEcpApi.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class BrentController : ControllerBase
    {
        private readonly IBrentService _service;
        private readonly IAuthService _serviceAuth;

        public BrentController(testContext context)
        {
            _service = new BrentService(context);
            _serviceAuth = new AuthService(context);

        }
        //********************* INICIO API REST ****************************************************
        //GET /api/brent/
        /// <summary>
        /// Recibe usuario y clave en la cabecera del mensaje y si es valida la autenticación
        /// retorna el atributo token con el valor generado. Valor vacio en caso contrario.
        /// </summary>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult<BrentDto>> getBrent()
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            // retorna listado
            BrentDto brentdto = new BrentDto();
            CrudoBrent brent = new CrudoBrent();

            brent = await _service.GetBrent();

            brentdto.Price = brent.data.price;
            brentdto.created_at = brent.data.created_at;
            return brentdto;
        }

        //********************* FIN API REST ****************************************************

        /// <summary>
        /// Valida el Header del mensaje (usuario y token) contra la BD. 
        /// retorna TRUE si valida el acceso. FALSE en caso contrario.
        /// </summary>
        private async Task<Boolean> ValidateToken()
        {
            var headtoken = HttpContext.Request.Headers["Token"];
            var headusuario = HttpContext.Request.Headers["Usuario"];

            var usuario = await _serviceAuth.GetUsuarioById(headusuario);

            //Leer de la BD el usaurio que coincida con el username
            if (usuario == null)
            {
                return (false);
            };

            //Se valida usuario y token
            if (usuario.Token != headtoken || usuario.Registro != headusuario)
            {
                return (false);
            };

            return true;
        }


    }
}