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
    class VisitRepositoryTest
    {
        private readonly VisitRepository repository = new VisitRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Visit testVisit = new Visit
            {
                DormitoryId = 2,
                ID = random.Next(),
                ResidentId = random.Next(),
                GuestId = random.Next(),
                GuardId = random.Next(),
                IsConfirmed = false,
                IsOver = false,
                VisitEndDateTime = DateTime.Now,
                VisitRegDateTime = DateTime.UtcNow
            };
            HttpResponseMessage response = repository.Create(testVisit);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Visit createdVisit = JsonConvert.DeserializeObject<Visit>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Visit creation test has failed.");
            Assert.Equals(testVisit, createdVisit);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable VisitID
            Visit response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Visit> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Visit deletedVisit = JsonConvert.DeserializeObject<Visit>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Visit deletion test has failed.");
            Assert.Equals(deletedVisit.ID, id);
        }
    }
}
