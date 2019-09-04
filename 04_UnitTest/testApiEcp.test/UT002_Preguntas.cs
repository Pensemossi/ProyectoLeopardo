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
    public class UT002_Preguntas
    {
        string urlBase = "https://localhost:44359/";

        [TestMethod]
        public async Task UT002_01_ConsultarPreguntas()
        {
            //Login para tener un token valido
            await Login();

            List<Categorias> categorias = new List<Categorias>();

            //Probar Consulta de todos los elementos de Categoria
            await CallHttpResponse.fn_CallHttpResponse("GETLIST", urlBase, "api/categoria", "C100021", "", MessageHttpResponse.token, "");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status, "Fallo: Probar Consulta de todos los elementos de Categoria - HttpStatusCode...");
            string r = MessageHttpResponse.Content;
            categorias = JsonConvert.DeserializeObject<List<Categorias>>(MessageHttpResponse.Content);
            Assert.AreNotEqual(categorias.Count, 0, "Fallo: Probar Consulta de todos los elementos Categorias - AreNotEqual...");

            //Leer preguntas de la categoria Nro 1
            List<Preguntas> preguntas = new List<Preguntas>();
            await CallHttpResponse.fn_CallHttpResponse("GETLIST", urlBase, "api/pregunta/pregunta/1/filtro/cantidad", "C100021", "", MessageHttpResponse.token, "");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status, "Fallo: Probar Consulta de todos los elementos de Preguntas categoria Nro 1 - HttpStatusCode...");
            r = MessageHttpResponse.Content;
            preguntas = JsonConvert.DeserializeObject<List<Preguntas>>(MessageHttpResponse.Content);
            Assert.AreNotEqual(preguntas.Count, 0, "Fallo: Probar Consulta de todos los elementos Preguntas categoria Nro 1  - AreNotEqual...");

  
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
