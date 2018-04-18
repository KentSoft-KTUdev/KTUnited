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
    public class RoomRepository : IRepository<Room>
    {

        public HttpResponseMessage Create(Room entity)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync("api/Rooms", content).Result;
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
                Room room = new Room();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.DeleteAsync("api/Rooms/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    room = JsonConvert.DeserializeObject<Room>(jsonContents);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Room> GetAll()
        {
            try
            {
                List<Room> listOfRooms = new List<Room>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Rooms").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfRooms = JsonConvert.DeserializeObject<List<Room>>(jsonContents);
                }
                return listOfRooms;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Room Read(object id)
        {
            try
            {
                Room room = new Room();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Rooms/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    room = JsonConvert.DeserializeObject<Room>(jsonContents);
                }
                return room;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Update(object id, Room entity)
        {
            try
            {
                Room room = new Room();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(new JavaScriptSerializer().Serialize(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync("api/Rooms/" + id.ToString(), content).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    room = JsonConvert.DeserializeObject<Room>(jsonContents);
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
