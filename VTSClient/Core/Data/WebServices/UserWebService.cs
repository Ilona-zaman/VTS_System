using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCutting;
using Core.Data.Model;
using Core.Data.WebServices.Interfaces;
using Core.Exceptions;
using Newtonsoft.Json;

namespace Core.Data.WebServices
{
    public class UserWebService : IUserWebService
    {
        private HttpClient _httpClient;

        public UserWebService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Configuration.BaseAddress);
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            try
            {
                string json = JsonConvert.SerializeObject(loginModel);
                var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync("/vts/signin", httpContent);
                var resultJson = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultContainer>(resultJson).ResultCode == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public async Task<bool> Ping()
        {
            try
            {
                var http = new HttpClient();
                http.BaseAddress = new Uri(Configuration.BaseAddress);
                http.Timeout = TimeSpan.FromSeconds(2);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var result = await http.GetAsync("/vts/ping");
                var resultJson = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ResultContainer>(resultJson).ResultCode == 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
