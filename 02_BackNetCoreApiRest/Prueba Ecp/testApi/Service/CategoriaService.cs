using System.Collections.Generic;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace testEcpApi.Service
{
    public class CategoriaService : ICategoriaService
    {
        private readonly testContext _context;
        public CategoriaService(testContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Categorias>>> getAll()
        {
            return await _context.DbCategorias.ToListAsync();
        }
        public async Task<ActionResult<Categorias>> GetById(int id)
        {
            return await _context.DbCategorias.FindAsync(id);
        }
        public void Create(Categorias categoria)
        {
            _context.DbCategorias.Add(categoria);
            _context.SaveChanges();

        }
        public void Put(Categorias categoria)
        {
            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

        }
        public void Delete(Categorias categoria)
        {
            _context.DbCategorias.Remove(categoria);
             _context.SaveChanges();

        }

    }

    public interface ICategoriaService
    {
        Task<ActionResult<IEnumerable<Categorias>>> getAll();
        Task<ActionResult<Categorias>> GetById(int id);
        void Put(Categorias categoria);
        void Delete(Categorias categoria);
        void Create(Categorias categoria);

    }
}
