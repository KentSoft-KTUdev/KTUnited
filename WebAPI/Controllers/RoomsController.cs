using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataModels;
using WebAPI.Extensions;
using RoomContract = DataContract.Objects.Room;

namespace WebAPI.Controllers
{
    /// <summary>
    /// All possible interactions within Dormitories API
    /// </summary>
    public class RoomsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Rooms
        /// <summary>
        /// Returns JSON serialized array of Rooms objects
        /// </summary>
        /// <returns>List of Rooms</returns>
        public List<RoomContract> GetRoomSet()
        {
            return db.RoomSet.ToList().Select(x => x.ToContract()).ToList();
        }

        // GET: api/Rooms/5
        /// <summary>
        /// Returns JSON seriliazed Room object determined by identification key
        /// </summary>
        /// <param name="id">Room identifaction key</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(RoomContract))]
        public IHttpActionResult GetRoom(int id)
        {
            RoomContract room = db.RoomSet.Find(id).ToContract();
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        /// <summary>
        /// Updates particular Room object in SQL database
        /// </summary>
        /// <param name="id">Room to be updated identifaction key</param>
        /// <param name="room">Updated Room object which will replace existing Room object in SQL database</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, RoomContract room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Number)
            {
                return BadRequest();
            }

            db.Entry(room.ToInternal()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        /// <summary>
        /// Inserts new Room object into SQL database
        /// </summary>
        /// <param name="room">Room to be inserted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(RoomContract))]
        public IHttpActionResult PostRoom(RoomContract room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Room temp = room.ToInternal();
            db.DormitorySet.Attach(temp.Dormitory);
            db.RoomSet.Add(temp);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RoomExists(room.Number))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = room.Number }, room);
        }

        // DELETE: api/Rooms/5
        /// <summary>
        /// Deletes existing Room object in SQL database
        /// </summary>
        /// <param name="id">Identification key of Room to be deleted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(RoomContract))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room temp = db.RoomSet.Find(id);
            RoomContract room = temp.ToContract();
            if (room == null)
            {
                return NotFound();
            }

            db.RoomSet.Remove(temp);
            db.SaveChanges();

            return Ok(room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.RoomSet.Count(e => e.Number == id) > 0;
        }
    }
}