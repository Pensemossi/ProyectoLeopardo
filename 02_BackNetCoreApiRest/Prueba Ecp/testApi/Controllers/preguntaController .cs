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
    public class preguntaController : ControllerBase
    {
        private readonly IPreguntaService _servicePregunta;
        private readonly IAuthService _serviceAuth;

        public preguntaController(testContext context)
        {
            _servicePregunta = new PreguntaService(context);
            _serviceAuth = new AuthService(context);
        }

        //********************* INICIO API REST ****************************************************
        // GET: api/pregunta
        /// <summary>
        /// retorna el listado de todas las preguntas sin filtrado
        /// </summary>
        [HttpGet]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult<IEnumerable<Preguntas>>> GetPregunta()
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            // retorna listado
            return await _servicePregunta.getAll();
        }

        // GET: api/pregunta/categoriaId
        /// <summary>
        /// retorna el listado de todas las preguntas según la categoria
        /// </summary>
        [HttpGet("pregunta/{CategoriaId}/filtro/{filter}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult<IEnumerable<Preguntas>>> GetPreguntaCategoria(Int32 CategoriaId, string filter)
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            // retorna listado
            return await _servicePregunta.GetByCategoriaId(CategoriaId, filter);
        }

        // POST: api/preguna
        /// <summary>
        /// Adiciona una pregunta según JSON enviado en el cuerpo del mensaje
        /// </summary>
        [HttpPost]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<ActionResult<Preguntas>> PostPregunta(Preguntas item)
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            //Adicionar item a la BD
            _servicePregunta.Create(item);

            return CreatedAtAction(nameof(GetPregunta), new { id = item.Id }, item);
        }
        // PUT: api/pregunta/#
        /// <summary>
        /// actualiza una pregunta correspondiente al Id= #. Los valores a actualizar en el cuerpo del mensaje
        /// </summary>
        [HttpPut("{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<IActionResult> PutCiudad(int id, Preguntas item)
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
            _servicePregunta.Put(item);

            return Ok();
        }
        // DELETE: api/pregunta/#
        /// <summary>
        /// Elimina una pegunta con ID= #.
        /// </summary>
        [HttpDelete("{id}")]
        [Microsoft.AspNetCore.Cors.EnableCors("Cors Politica")]
        public async Task<IActionResult> DeletePregunta(Int32 id)
        {
            //Si el mensaje no se recibe con el usuario y token correcto, rechaza la petición
            if (await ValidateToken() == false)
            {
                return Unauthorized();
            }

            // intentar eliminar el registro
            try
            {

                var Item = _servicePregunta.GetById(id);

                if (Item == null)
                {
                    return NotFound();
                }

                _servicePregunta.Delete(Item.Result.Value);
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

