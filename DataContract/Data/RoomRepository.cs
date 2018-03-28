using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.Objects;
using DataContract.Interfaces;
using DataContract.Data;
using System.Net.Http;
using Newtonsoft.Json;

namespace DataContract.Data
{
    public class RoomRepository : IRepository<Room>
    {
        public Boolean Create(Room entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(object id)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAll()
        {
            try
            {
                List<Room> listOfRooms = new List<Room>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://webapi-kentsoft.azurewebsites.net/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Rooms").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfRooms = JsonConvert.DeserializeObject<List<Room>>(jsonContents);
                }
                return listOfRooms;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Room Read(object id)
        {
            try
            {
                Room room = new Room();
                int _id = (int)id;
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("http://webapi-kentsoft.azurewebsites.net/");
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(String.Format("api/Rooms/{0}", id)).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    room = JsonConvert.DeserializeObject<Room>(jsonContents);
                }
                return room;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(object id, Room entity)
        {
            throw new NotImplementedException();
        }
    }
}
