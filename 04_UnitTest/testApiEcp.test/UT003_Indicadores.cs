using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using testEcpApi.Models;

namespace UT002_Preguntas
{

    [TestClass]
    public class UT003_Indicadores
    {
        string urlBase = "https://localhost:44359/";

        [TestMethod]
        public async Task UT003_01Indicadores()
        {
            //Login para tener un token valido
            await Login();

            //Leer precio del brent
            await CallHttpResponse.fn_CallHttpResponse("GET", urlBase, "api/brent" , "C100021", "", MessageHttpResponse.token, "");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status, "consultar elemento Brent - HttpStatusCode...");

            //Leer precio de la accion
            await CallHttpResponse.fn_CallHttpResponse("GET", urlBase, "api/accion", "C100021", "", MessageHttpResponse.token, "");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status, "consultar elemento Precio Acciòn - HttpStatusCode...");

            //Leer temperatura
            await CallHttpResponse.fn_CallHttpResponse("GET", urlBase, "api/temperatura", "C100021", "", MessageHttpResponse.token, "");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status, "consultar elemento Temperatura - HttpStatusCode...");

            return;
        }
 
        public async Task Login()
        {
            
            Usuarios usuario = new Usuarios();
            await CallHttpResponse.fn_CallHttpResponse("POST", urlBase, "api/Auth", "C100021", "12345", "", "");
            usuario = JsonConvert.DeserializeObject<Usuarios>(MessageHttpResponse.Content);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status);
            MessageHttpResponse.token = usuario.Token;
        }
    }
}
