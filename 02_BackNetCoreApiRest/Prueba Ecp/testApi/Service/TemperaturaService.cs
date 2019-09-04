using System.Linq;
using System.Threading.Tasks;
using testEcpApi.Models;
using testEcpApi.Data;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace testEcpApi.Service
{
    public class TemperaturaService : ITemperaturaService
    {
        private readonly testContext _context;
        public TemperaturaService(testContext context)  
        {
            _context = context;

        }
        public async Task<Temperatura> GetTemperatura(decimal Lat, decimal Lon)
        {

            string urlBase = "https://api.openweathermap.org/data/2.5/weather?lat=" + Lat.ToString().Replace(",",".") + "&lon=" + Lon.ToString().Replace(",", ".") + "&units=metric&appid=bd6fed932d141bb4ca83aaa91ded7ff4 ";
            Temperatura temperatura = new Temperatura();

            await CallHttpResponse.fn_CallHttpResponse("GET", urlBase, "", "Ecp", "");
            string result = MessageHttpResponse.Content;
            temperatura = JsonConvert.DeserializeObject<Temperatura>(MessageHttpResponse.Content);

            //MessageHttpResponse.Status
            return temperatura;

        }


    }
    public interface ITemperaturaService
    {
        Task<Temperatura> GetTemperatura(decimal Lat, decimal Lon);

    }
}
