using DataContract.Data;
using DataContract.Objects;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataContract.Test
{
    [TestFixture]
    class GuestRepositoryTest
    {
        private readonly GuestRepository repository = new GuestRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Guest testGuest = new Guest
            {
                Name = Generation.GetRandomAlphaNumeric(),
                PersonalCode = random.Next(),
                Surname = Generation.GetRandomAlphaNumeric()
            };
            HttpResponseMessage response = repository.Create(testGuest);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Guest createdGuest = JsonConvert.DeserializeObject<Guest>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Guest creation test has failed.");
            Assert.Equals(testGuest, createdGuest);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable adminID
            Guest response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Guest> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Guest deletedGuest = JsonConvert.DeserializeObject<Guest>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Guest deletion test has failed.");
            Assert.Equals(deletedGuest.PersonalCode, id);
        }
    }
}
