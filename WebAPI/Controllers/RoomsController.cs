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
        public List<Room> GetRoomSet()
        {
            return db.RoomSet.ToList();
        }

        // GET: api/Rooms/5
        /// <summary>
        /// Returns JSON seriliazed Room object determined by identification key
        /// </summary>
        /// <param name="id">Room identifaction key</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = db.RoomSet.Find(id);
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
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Number)
            {
                return BadRequest();
            }

            db.Entry(room).State = EntityState.Modified;

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
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.RoomSet.Add(room);

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
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.RoomSet.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            db.RoomSet.Remove(room);
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