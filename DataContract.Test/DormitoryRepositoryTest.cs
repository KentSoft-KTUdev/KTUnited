using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataContract.Data;
using DataContract.Objects;
using System.Net.Http;
using Newtonsoft.Json;

namespace DataContract.Test
{
    [TestFixture]
    class DormitoryRepositoryTest
    {
        private readonly DormitoryRepository repository = new DormitoryRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Dormitory testDormitory = new Dormitory
            {
                ID = random.Next(),
                Name = Generation.GetRandomAlphaNumeric(),
                Adress = Generation.GetRandomAlphaNumeric(),
            };
            HttpResponseMessage response = repository.Create(testDormitory);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Dormitory createdDormitory = JsonConvert.DeserializeObject<Dormitory>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Dormitory creation test has failed.");
            Assert.Equals(testDormitory, createdDormitory);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable adminID
            Dormitory response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Dormitory> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Dormitory deletedDormitory = JsonConvert.DeserializeObject<Dormitory>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Dormitory deletion test has failed.");
            Assert.Equals(deletedDormitory.ID, id);
        }
    }
}
