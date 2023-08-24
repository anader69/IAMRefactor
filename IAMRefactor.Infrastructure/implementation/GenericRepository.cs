using IAMRefactor.Application.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Infrastructure.implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public async Task<dynamic> Post(T model, string URL)
        {
            var dataString = JsonConvert.SerializeObject(model);

            var buffer = Encoding.UTF8.GetBytes(dataString);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");


            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.PostAsync(URL, byteContent).Result;
            // Blocking call! Program will wait here until a response is received or a timeout occurs.
            client.Dispose();
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(responseBody);
                return obj;

            }
            else
            {
                string responseBody = response.StatusCode.ToString();
                return null;
            }


        }
    }
}
