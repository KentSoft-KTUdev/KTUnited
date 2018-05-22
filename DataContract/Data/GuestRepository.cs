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
//using System.Web.Script.Serialization;

namespace DataContract.Data
{
    public class GuestRepository : IRepository<Guest>
    {

        public HttpResponseMessage Create(Guest entity)
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PostAsync("api/Guests", content).Result;
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
                Guest guest = new Guest();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.DeleteAsync("api/Guests/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    guest = JsonConvert.DeserializeObject<Guest>(jsonContents);
                    return response;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Guest> GetAll()
        {
            try
            {
                List<Guest> listOfGuests = new List<Guest>();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Guests").Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    listOfGuests = JsonConvert.DeserializeObject<List<Guest>>(jsonContents);
                }
                return listOfGuests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Guest> GetResidentGuests(long residentPersonalCode)
        {
            VisitRepository visitRepository = new VisitRepository();
            List<Visit> visits = visitRepository.GetResidentVisits(residentPersonalCode).Distinct(new VisitEqualityComparerByGuest()).ToList();
            List<Guest> guests = new List<Guest>();
            foreach (Visit visit in visits)
            {
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        Guest scopeGuest = new Guest();
                        httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                        httpClient.DefaultRequestHeaders.Accept.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                        HttpResponseMessage response = httpClient.GetAsync("api/Guests/" + visit.GuestId).Result;
                        response.EnsureSuccessStatusCode();
                        string jsonContents = response.Content.ReadAsStringAsync().Result;
                        scopeGuest = JsonConvert.DeserializeObject<Guest>(jsonContents);
                        guests.Add(scopeGuest);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return guests;
        }

        public Guest Read(object id)
        {
            try
            {
                Guest guest = new Guest();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync("api/Guests/" + id.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    guest = JsonConvert.DeserializeObject<Guest>(jsonContents);
                }
                return guest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HttpResponseMessage Update(object id, Guest entity)
        {
            try
            {
                Guest guest = new Guest();
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(Configuration.WebApiAdress);
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = httpClient.PutAsync("api/Guests/" + id.ToString(), content).Result;
                    response.EnsureSuccessStatusCode();
                    string jsonContents = response.Content.ReadAsStringAsync().Result;
                    guest = JsonConvert.DeserializeObject<Guest>(jsonContents);
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
