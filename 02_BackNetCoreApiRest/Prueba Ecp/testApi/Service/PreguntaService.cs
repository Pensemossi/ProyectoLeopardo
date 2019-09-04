using System.Collections.Generic;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace testEcpApi.Service
{
    public class PreguntaService : IPreguntaService
    {
        private readonly testContext _context;
        public PreguntaService(testContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Preguntas>>> getAll()
        {
            return await _context.DbPreguntas.ToListAsync();
        }
        public async Task<ActionResult<Preguntas>> GetById(int id)
        {
            return await _context.DbPreguntas.FindAsync(id);
        }
        public  async Task<ActionResult<IEnumerable<Preguntas>>> GetByCategoriaId(int categoriaId, string filter)
        {

            var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();
            var command = conn.CreateCommand();
            string query = "select Id, CategoriaId, Pregunta, Respuesta, Activa from Preguntas where Activa = 'S' " +
                           " and categoriaId = " + categoriaId + 
                           " and upper(Pregunta) like '%" + filter.ToUpper() +"%'";

            command.CommandText = query;
            var reader = await command.ExecuteReaderAsync();

            List<Preguntas> preguntas = new List<Preguntas>();
            Preguntas pregunta;

            while (await reader.ReadAsync())
            {
                pregunta = new Preguntas();
                pregunta.Id = reader.GetInt32(0);
                pregunta.CategoriaId = reader.GetInt32(1);
                pregunta.Pregunta = reader.GetString(2);
                pregunta.Respuesta = reader.GetString(3);
                pregunta.Activa = reader.GetString(4);

                preguntas.Add(pregunta);
            }

            return preguntas;  
        }

        public void Create(Preguntas pregunta)
        {
            _context.DbPreguntas.Add(pregunta);
            _context.SaveChanges();

        }
        public void Put(Preguntas pregunta)
        {
            _context.Entry(pregunta).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public void Delete(Preguntas pregunta)
        {
            _context.DbPreguntas.Remove(pregunta);
             _context.SaveChanges();

        }

    }

    public interface IPreguntaService
    {
        Task<ActionResult<IEnumerable<Preguntas>>> getAll();
        Task<ActionResult<Preguntas>> GetById(int id);
        Task<ActionResult<IEnumerable<Preguntas>>> GetByCategoriaId(int categoriaId, string filter);
        void Put(Preguntas pregunta);
        void Delete(Preguntas pregunta);
        void Create(Preguntas pregunta);

    }
}
