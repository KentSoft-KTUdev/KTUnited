using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using DataContract.Data;
using DataContract.Objects;
using System.Net.Http;
using Newtonsoft.Json;

namespace DataContract.Test
{
    [TestFixture]
    public class AdministratorRepositoryTest
    {
        private readonly AdministratorRepository repository = new AdministratorRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Administrator testAdmin = new Administrator
            {
                DormitoryId = 2,
                Name = Generation.GetRandomAlphaNumeric(),
                Password = Generation.GetRandomAlphaNumeric(),
                PersonalCode = random.Next(),
                Surname = Generation.GetRandomAlphaNumeric(),
                Username = Generation.GetRandomAlphaNumeric()
            };
            HttpResponseMessage response = repository.Create(testAdmin);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Administrator createdAdmin = JsonConvert.DeserializeObject<Administrator>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Admnistrator creation test has failed.");
            Assert.Equals(testAdmin, createdAdmin);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable adminID
            Administrator response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Administrator> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Administrator deletedAdmin = JsonConvert.DeserializeObject<Administrator>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Admnistrator deletion test has failed.");
            Assert.Equals(deletedAdmin.PersonalCode, id);
        }
    }
}
