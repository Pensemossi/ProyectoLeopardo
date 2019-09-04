using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using testEcpApi.Models;

namespace testEcpApi.Models
{

    public static class MessageHttpResponse
    {
        public static string token;
        public static System.Net.HttpStatusCode Status { get; set; }
        public static string Content { get; set; }
    }

    public static class CallHttpResponse
    {

        public static async Task fn_CallHttpResponse(string strType, string strBase, string strPath,  string token, string body)
        {

            HttpClient client = new HttpClient();
            MessageHttpResponse.Content = "";
            MessageHttpResponse.Status = 0;

            client.BaseAddress = new Uri(strBase);
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var buffer = System.Text.Encoding.UTF8.GetBytes(body);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            client.DefaultRequestHeaders.Add("Authorization", token);

            switch (strType)
            {
                case "GET":
                    var response = client.GetAsync(strPath).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response.Content.ReadAsStringAsync();
                        customerJsonString = await response.Content.ReadAsStringAsync();
                        MessageHttpResponse.Content = customerJsonString;

                    }
                    MessageHttpResponse.Status = response.StatusCode;
                    break;
                case "GETLIST":
                    var response2 = client.GetAsync(strPath).Result;
                    if (response2.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response2.Content.ReadAsStringAsync();
                        customerJsonString = await response2.Content.ReadAsStringAsync();
                        MessageHttpResponse.Content = customerJsonString;
                    }
                    MessageHttpResponse.Status = response2.StatusCode;
                    break;
                case "POST":
                    var response3 = client.PostAsync(strPath, byteContent).Result;
                    if (response3.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response3.Content.ReadAsStringAsync();
                        customerJsonString = await response3.Content.ReadAsStringAsync();
                        MessageHttpResponse.Content = customerJsonString;
                    }
                    MessageHttpResponse.Status = response3.StatusCode;
                    break;
                case "PUT":
                    var response4 = client.PutAsync(strPath, byteContent).Result;
                    if (response4.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response4.Content.ReadAsStringAsync();
                        customerJsonString = await response4.Content.ReadAsStringAsync();
                        MessageHttpResponse.Content = customerJsonString;
                    }
                    MessageHttpResponse.Status = response4.StatusCode;
                    break;
                case "DELETE":
                    var response5 = client.DeleteAsync(strPath).Result;
                    if (response5.IsSuccessStatusCode)
                    {
                        var customerJsonString = await response5.Content.ReadAsStringAsync();
                        customerJsonString = await response5.Content.ReadAsStringAsync();
                        MessageHttpResponse.Content = customerJsonString;

                    }
                    MessageHttpResponse.Status = response5.StatusCode;
                    break;
            }
            return;

        }

    }

}
