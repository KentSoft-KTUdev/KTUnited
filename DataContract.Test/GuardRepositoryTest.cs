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
    class GuardRepositoryTest
    {
        private readonly GuardRepository repository = new GuardRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Guard testGuard = new Guard
            {
                DormitoryId = 2,
                Name = Generation.GetRandomAlphaNumeric(),
                Password = Generation.GetRandomAlphaNumeric(),
                PersonalCode = random.Next(),
                Surname = Generation.GetRandomAlphaNumeric(),
                Username = Generation.GetRandomAlphaNumeric()
            };
            HttpResponseMessage response = repository.Create(testGuard);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Dormitory createdGuard = JsonConvert.DeserializeObject<Dormitory>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Guard creation test has failed.");
            Assert.Equals(testGuard, createdGuard);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable adminID
            Guard response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Guard> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Guard deletedGuard = JsonConvert.DeserializeObject<Guard>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Guard deletion test has failed.");
            Assert.Equals(deletedGuard.PersonalCode, id);
        }
    }
}
