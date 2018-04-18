using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataContract.Interfaces;
using DataContract.Objects;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Web.Script.Serialization;

namespace DataContract.Data
{
    public class VisitRepository : IRepository<Visit>
    {

        public HttpResponseMessage Create(Visit entity)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync("api/Visits", content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response;
                    }
                    else
                    {
                        return response;
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Delete(object id)
        {
            try
            {
                Visit visit = new Visit();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.DeleteAsync("api/Visits/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    visit = JsonConvert.DeserializeObject<Visit>(jsonContents);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Visit> GetAll()
        {
            try
            {
                List<Visit> listOfVisits = new List<Visit>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Visits").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfVisits = JsonConvert.DeserializeObject<List<Visit>>(jsonContents);
                }
                return listOfVisits;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Visit Read(object id)
        {
            try
            {
                Visit visit = new Visit();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Visits/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    visit = JsonConvert.DeserializeObject<Visit>(jsonContents);
                }
                return visit;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Update(object id, Visit entity)
        {
            try
            {
                Visit visit = new Visit();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync("api/Visits/" + id.ToString(), content).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    visit = JsonConvert.DeserializeObject<Visit>(jsonContents);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
