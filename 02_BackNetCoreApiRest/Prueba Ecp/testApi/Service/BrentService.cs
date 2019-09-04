using System.Linq;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace testEcpApi.Service
{
    public class BrentService : IBrentService
    {
        private readonly testContext _context;
        public BrentService(testContext context)  
        {
            _context = context;

        }
        public async Task<CrudoBrent> GetBrent()
        {
            string urlBase = "https://api.oilpriceapi.com/v1/prices/latest";
            CrudoBrent crudoBrent = new CrudoBrent();

            await CallHttpResponse.fn_CallHttpResponse("GET", urlBase, "", "Token e87c5ae8beee22b90e98b756795fdb97", "");
            string result = MessageHttpResponse.Content;
            crudoBrent = JsonConvert.DeserializeObject<CrudoBrent>(MessageHttpResponse.Content);
            System.DateTime fechahora = crudoBrent.data.created_at.AddHours(-5);
            crudoBrent.data.created_at = fechahora;

            //MessageHttpResponse.Status
            return crudoBrent;

        }


    }
    public interface IBrentService 
    {
        Task<CrudoBrent> GetBrent();

    }
}
