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
    public class DormitoryRepository : IRepository<Dormitory>
    {

        public HttpResponseMessage Create(Dormitory entity)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync("api/Dormitories", content).Result;
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
                Dormitory dormitory = new Dormitory();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.DeleteAsync("api/Dormitories/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    dormitory = JsonConvert.DeserializeObject<Dormitory>(jsonContents);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Dormitory> GetAll()
        {
            try
            {
                List<Dormitory> listOfDormitories = new List<Dormitory>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Dormitories").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfDormitories = JsonConvert.DeserializeObject<List<Dormitory>>(jsonContents);
                }
                return listOfDormitories;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dormitory Read(object id)
        {
            try
            {
                Dormitory dormitory = new Dormitory();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Dormitories/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    dormitory = JsonConvert.DeserializeObject<Dormitory>(jsonContents);
                }
                return dormitory;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Update(object id, Dormitory entity)
        {
            try
            {
                Dormitory dormitory = new Dormitory();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync("api/Dormitories/" + id.ToString(), content).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    dormitory = JsonConvert.DeserializeObject<Dormitory>(jsonContents);
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
