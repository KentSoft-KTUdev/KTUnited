using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataContract.Interfaces;
using DataContract.Objects;
using Newtonsoft.Json;

namespace DataContract.Data
{
    public class DormitoryRepository : IRepository<Dormitory>
    {

        public bool Create(Dormitory entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public List<Dormitory> GetAll()
        {
            try
            {
                List<Dormitory> listOfDormitories = new List<Dormitory>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://webapi-kentsoft.azurewebsites.net/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Dormitories").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfDormitories = JsonConvert.DeserializeObject<List<Dormitory>>(jsonContents);
                }
                return listOfDormitories;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Dormitory Read(object id)
        {
            try
            {
                Dormitory dormitory = new Dormitory();
                int _id = (int)id;
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://webapi-kentsoft.azurewebsites.net/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(String.Format("api/Rooms/{0}", id)).Result;
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

        public bool Update(object id, Dormitory entity)
        {
            throw new NotImplementedException();
        }
    }
}
