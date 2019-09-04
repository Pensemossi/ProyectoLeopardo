using System.Linq;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace testEcpApi.Service
{
    public class AccionEcpService : IAccionECPService
    {
        private readonly testContext _context;
        public AccionEcpService(testContext context)  
        {
            _context = context;

        }
        public async Task<AccionECP> GetAccionEcp()
        {
            string urlBase = "https://cloud.iexapis.com/stable/stock/ec/quote?token=pk_0a497c0131984e8ba0d26422d0ea958c";
            AccionECP accion= new AccionECP();

            await CallHttpResponse.fn_CallHttpResponse("GET", urlBase, "", "ecp", "");
            string result = MessageHttpResponse.Content;
            accion = JsonConvert.DeserializeObject<AccionECP>(MessageHttpResponse.Content);

            //MessageHttpResponse.Status
            return accion;

        }


    }
    public interface IAccionECPService 
    {
        Task<AccionECP> GetAccionEcp();

    }
}
