using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAPI.Interfaces;
using WebAPI.DataModels;
using System.Net.Http;
using Newtonsoft.Json;

namespace WebAPI.Data
{
    public class RoomRepository : IRepository<Room>
    {
        public Room Create(Room entity)
        {
            throw new NotImplementedException();
        }

        public Guid Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Room> GetAll()
        {
            List<Room> listOfRooms = new List<Room>();
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://webapi-kentsoft.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("api/Rooms").Result;
                response.EnsureSuccessStatusCode();

                string jsonContents = response.Content.ReadAsStringAsync().Result;
                
            }
            return listOfRooms;
        }

        public Room Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public Room Update(Guid id, Room entity)
        {
            throw new NotImplementedException();
        }
    }
}