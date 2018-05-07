using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using DataContract.Interfaces;
using DataContract.Objects;
using Newtonsoft.Json;

namespace DataContract.Data
{
    public class AdministratorRepository : IRepository<Administrator>
    {
        public HttpResponseMessage Create(Administrator entity)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync("api/Administrators", content).Result;
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

        public HttpResponseMessage Login(string user, string pass)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Post, String.Format("api/Administrators?login={0}&password={1}", user, pass));
                    HttpResponseMessage response = httpClient.SendAsync(httpRequest).Result;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Delete(object id)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.DeleteAsync("api/Administrators/" + id.ToString()).Result;
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

        public List<Administrator> GetAll()
        {
            try
            {
                List<Administrator> listOfAdmins = new List<Administrator>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Administrators").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfAdmins = JsonConvert.DeserializeObject<List<Administrator>>(jsonContents);
                }
                return listOfAdmins;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Administrator Read(object id)
        {
            try
            {
                Administrator admin = new Administrator();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Administrators/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    admin = JsonConvert.DeserializeObject<Administrator>(jsonContents);
                }
                return admin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Administrator ReadByUsername(string user)
        {
            try
            {
                Administrator admin = new Administrator();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Administrators?user=" + user).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    admin = JsonConvert.DeserializeObject<Administrator>(jsonContents);
                }
                return admin;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Update(object id, Administrator entity)
        {
            try
            {
                Administrator admin = new Administrator();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync("api/Administrators/" + id.ToString(), content).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    admin = JsonConvert.DeserializeObject<Administrator>(jsonContents);
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
