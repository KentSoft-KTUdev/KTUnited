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
    class RoomRepositoryTest
    {
        private readonly RoomRepository repository = new RoomRepository();
        [Test]
        public void IsCreatable()
        {
            Random random = new Random(DateTime.Now.Millisecond);
            Room testRoom = new Room
            {
                ID = random.Next(),
                DormitoryId = 2,
                Number = random.Next()
            };
            HttpResponseMessage response = repository.Create(testRoom);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Room createdRoom = JsonConvert.DeserializeObject<Room>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Room creation test has failed.");
            Assert.Equals(testRoom, createdRoom);
        }
        [Test]
        public void IsGetable()
        {
            object id = 654456; //insert testable adminID
            Room response = repository.Read(id);
            Assert.NotNull(response);
        }
        [Test]
        public void IsGetableAll()
        {
            List<Room> list = repository.GetAll();
            Assert.NotNull(list, "List wasn't received");
        }
        [Test]
        public void IsDeleteable()
        {
            object id = 6546;//test object to be deleted id
            HttpResponseMessage response = repository.Delete(id);
            string jsonContents = response.Content.ReadAsStringAsync().Result;
            Room deletedRoom = JsonConvert.DeserializeObject<Room>(jsonContents);
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.Created, "Room deletion test has failed.");
            Assert.Equals(deletedRoom.ID, id);
        }
    }
}
