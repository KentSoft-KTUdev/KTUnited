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
    class ResidentRepositoryTest
    {
        private readonly ResidentRepository repository = new ResidentRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Resident testResident = new Resident
            {
                DormitoryId = 2,
                Name = Generation.GetRandomAlphaNumeric(),
                Password = Generation.GetRandomAlphaNumeric(),
                PersonalCode = random.Next(),
                Surname = Generation.GetRandomAlphaNumeric(),
                Username = Generation.GetRandomAlphaNumeric()
            };
            HttpResponseMessage response = repository.Create(testResident);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Resident createdResident = JsonConvert.DeserializeObject<Resident>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Resident creation test has failed.");
            Assert.Equals(testResident, createdResident);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable ResidentID
            Resident response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Resident> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Resident deletedResident = JsonConvert.DeserializeObject<Resident>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Resident deletion test has failed.");
            Assert.Equals(deletedResident.PersonalCode, id);
        }
    }
}
