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

namespace UT001_Auth
{

    [TestClass]
    public class UT001_Auth
    {
        string urlBase = "https://localhost:44359/";

        [TestMethod]
        public async Task UT001_Auth_01LoginFailed()
        {
            Usuarios u = new Usuarios();

            //Probar login 1
            await CallHttpResponse.fn_CallHttpResponse("POST", urlBase, "api/Auth", "C100020", "12345X", "", "");
            Assert.AreNotEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status);
        }

        [TestMethod]
        public async Task UT001_Auth_02LoginFailed()
        {
            Usuarios u = new Usuarios();

            //Probar login 1
            await CallHttpResponse.fn_CallHttpResponse("POST", urlBase, "api/Auth", "C100020X", "12345", "", "");
            Assert.AreNotEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status);
        }

        [TestMethod]
        public async Task UT001_Auth_03LoginOK()
        {
            Usuarios usuario = new Usuarios();

            //Probar login 1
            await CallHttpResponse.fn_CallHttpResponse("POST", urlBase, "api/Auth", "C100020", "12345", "", "");
            Assert.AreEqual(System.Net.HttpStatusCode.OK, MessageHttpResponse.Status);
            return;
        }


    }
}
