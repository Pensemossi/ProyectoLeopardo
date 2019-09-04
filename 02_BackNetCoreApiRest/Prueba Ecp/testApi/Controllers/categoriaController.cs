using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using testEcpApi.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testEcpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriaController : ControllerBase
    {
        private readonly ICategoriaService _serviceCategoria;
        private readonly IAuthService _serviceAuth;

        public categoriaController(testContext context)
        {
            _serviceCategoria = new CategoriaService(context);
            _serviceAuth = new AuthService(context);
        }

        //********************* INICIO API REST ****************************************************
        // GET: api/categoria
        /// <summary>
        /// retorna el listado de todas las categorias sin ningún filtro
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult<IEnumerable<Categorias>>> GetCategoria()
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            // retorna listado
            return await _serviceCategoria.getAll();
        }

        // POST: api/categoria
        /// <summary>
        /// Adiciona una categoria según JSON enviado en el cuerpo del mensaje
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult<Categorias>> PostCiudad(Categorias item)
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            //Adicionar item a la BD
            _serviceCategoria.Create(item);

            return CreatedAtAction(nameof(Categorias), new { id = item.Id }, item);
        }
        // PUT: api/categoria/#
        /// <summary>
        /// actualiza una catgoria correspondiente al Id= #. Los valores a actualizar en el cuerpo del mensaje
        /// </summary>
        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<IActionResult> PutCatgoria(int id, Categorias item)
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            //validación primaria
            if (id != item.Id)
            {
                return BadRequest();
            }

            //modificar registro en la BD
            _serviceCategoria.Put(item);

            return Ok();
        }
        // DELETE: api/categoria/#
        /// <summary>
        /// Elimina una categoria con ID= #.
        /// </summary>
        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<IActionResult> DeleteCiudad(Int32 id)
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            // intentar eliminar el registro
            try
            {

                var Item = _serviceCategoria.GetById(id);

                if (Item == null)
                {
                    return NotFound();
                }

                _serviceCategoria.Delete(Item.Result.Value);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok();
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

