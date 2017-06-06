using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.CrossCutting;
using Core.Data.Model;
using Core.Data.WebServices.Interfaces;
using Core.Exceptions;
using Newtonsoft.Json;

namespace Core.Data.WebServices
{
    public class VacationWebService : IVacationWebService
    {
        private readonly HttpClient _httpClient;

        public VacationWebService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(Configuration.BaseAddress);
        }

        public async Task<Vacation> CreateVacation(Vacation vacation)
        {
            try
            {
                var vacationJson = JsonConvert.SerializeObject(vacation);
                var content = new StringContent(vacationJson, Encoding.UTF8, "application/json");
                var result = await _httpClient.PostAsync("/vts/workflow/", content);
                vacationJson = result.Content.ReadAsStringAsync().Result;

                vacation = JsonConvert.DeserializeObject<ResultContainer<Vacation>>(vacationJson).ItemResult;

                return vacation;
            }
            catch (Exception exception)
            {
                throw new DataException(exception.Message, exception);
            }
        }

        public async Task<bool> UpdateVacation(Vacation vacation)
        {
            try
            {
                var vacationJson = JsonConvert.SerializeObject(vacation);
                var content = new StringContent(vacationJson, Encoding.UTF8, "application/json");

                var result = await _httpClient.PostAsync("/vts/workflow/", content);

                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Vacation>> GetVacations()
        {
            try
            {
                var result = await _httpClient.GetAsync("/vts/workflow/");
                var resultJson = result.Content.ReadAsStringAsync().Result;
                var vacations =
                    JsonConvert.DeserializeObject<ResultContainer<IEnumerable<Vacation>>>(resultJson).ListResult;

                return vacations;
            }
            catch (Exception exception)
            {
                throw new DataException(exception.Message, exception);
            }
        }

        public async Task<bool> DeleteVacations(Vacation vacation)
        {
            try
            {
                var uri = Path.Combine("/vts/workflow/", vacation.Id.ToString());
                var result = await _httpClient.DeleteAsync(uri);

                return result.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Vacation> GetVacation(string id)
        {
            try
            {
                var uri = Path.Combine("/vts/workflow/", id);
                var result = await _httpClient.GetAsync(uri);
                var resultJson = result.Content.ReadAsStringAsync().Result;

                var vacation = JsonConvert.DeserializeObject<ResultContainer<Vacation>>(resultJson).ItemResult;

                return vacation;
            }
            catch (Exception exception)
            {
                throw new DataException(exception.Message, exception);
            }
        }
    }
}