using System.Linq;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace testEcpApi.Service
{
    public class AuthService : IAuthService
    {
        private readonly testContext _context;
        public AuthService(testContext context)
        {
            _context = context;
        }
        public async Task<string> Authenticate(NetworkCredential credentials)
        {
            //Leer de la BD el usaurio que coincida con el username
            var usuario = await _context.DbUsuarios.FindAsync(credentials.UserName);
            if (usuario == null)
            {
                return ("");
            };

            //Se valida usuario y clave
            if (usuario.Clave != credentials.Password)
            {
                return ("");
            };

            //generar token
            string token = "";
            token = System.Guid.NewGuid().ToString();
            usuario.Token = token;

            //escribir Token en la BD
            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            //Retornar Token
            return token;
        }
        public async Task<Usuarios> GetUsuarioById(string Id)
        {
            return await _context.DbUsuarios.FindAsync(Id);
        }

    }
    public interface IAuthService 
    {
        Task<string> Authenticate(NetworkCredential credentials);
        Task<Usuarios> GetUsuarioById(string Id);

    }
}
