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
using GuestContract = DataContract.Objects.Guest;

namespace WebAPI.Controllers
{
    /// <summary>
    /// All possible interactions within Guests API
    /// </summary>
    public class GuestsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Guests
        /// <summary>
        /// Returns JSON serialized array of Guest objects
        /// </summary>
        /// <returns>List of Guests</returns>
        public List<GuestContract> GetGuestSet()
        {
            return db.GuestSet.ToList().Select(x => x.ToContract()).ToList();
        }

        // GET: api/Guests/5
        /// <summary>
        /// Returns JSON seriliazed Guest object determined by identification key
        /// </summary>
        /// <param name="id">Guest identifaction key</param>
        /// <returns>Http request result</returns>
        [ResponseType(typeof(GuestContract))]
        public IHttpActionResult GetGuest(long id)
        {
            GuestContract guest = db.GuestSet.Find(id).ToContract();
            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        // PUT: api/Guests/5
        /// <summary>
        /// Updates particular Guest object in SQL database
        /// </summary>
        /// <param name="id">Guest to be updated identifaction key</param>
        /// <param name="guest">Updated Guest object which will replace existing Guest object in SQL database</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuest(long id, GuestContract guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guest.PersonalCode)
            {
                return BadRequest();
            }

            db.Entry(guest.ToInternal()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
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

        // POST: api/Guests
        /// <summary>
        /// Inserts new Guest object into SQL database
        /// </summary>
        /// <param name="guest">Guest to be inserted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(GuestContract))]
        public IHttpActionResult PostGuest(GuestContract guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guest temp = guest.ToInternal();
            db.GuestSet.Add(temp);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GuestExists(guest.PersonalCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = guest.PersonalCode }, guest);
        }

        // DELETE: api/Guests/5
        /// <summary>
        /// Deletes existing Guest object in SQL database
        /// </summary>
        /// <param name="id">Identification key of Guest to be deleted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(GuestContract))]
        public IHttpActionResult DeleteGuest(long id)
        {
            Guest temp = db.GuestSet.Find(id);
            GuestContract guest = temp.ToContract();
            if (guest == null)
            {
                return NotFound();
            }

            db.GuestSet.Remove(temp);
            db.SaveChanges();

            return Ok(guest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestExists(long id)
        {
            return db.GuestSet.Count(e => e.PersonalCode == id) > 0;
        }
    }
}