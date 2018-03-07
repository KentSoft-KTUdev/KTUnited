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
    /// All possible interactions within Residents API
    /// </summary>
    public class ResidentsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Residents
        /// <summary>
        /// Returns JSON serialized array of Resident objects
        /// </summary>
        /// <returns>List of Residents</returns>
        public List<Resident> GetResidentSet()
        {
            return db.ResidentSet.ToList();
        }

        // GET: api/Residents/5
        /// <summary>
        /// Returns JSON seriliazed Resident object determined by identification key
        /// </summary>
        /// <param name="id">Resident identifaction key</param>
        /// <returns>Http request result</returns>
        [ResponseType(typeof(Resident))]
        public IHttpActionResult GetResident(long id)
        {
            Resident resident = db.ResidentSet.Find(id);
            if (resident == null)
            {
                return NotFound();
            }

            return Ok(resident);
        }

        // PUT: api/Residents/5
        /// <summary>
        /// Updates particular Resident object in SQL database
        /// </summary>
        /// <param name="id">Resident to be updated identifaction key</param>
        /// <param name="resident">Updated Resident object which will replace existing Resident object in SQL database</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutResident(long id, Resident resident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != resident.PersonalCode)
            {
                return BadRequest();
            }

            db.Entry(resident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResidentExists(id))
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

        // POST: api/Residents
        /// <summary>
        /// Inserts new Resident object into SQL database
        /// </summary>
        /// <param name="resident">Resident to be inserted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(Resident))]
        public IHttpActionResult PostResident(Resident resident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ResidentSet.Add(resident);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ResidentExists(resident.PersonalCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = resident.PersonalCode }, resident);
        }

        // DELETE: api/Residents/5
        /// <summary>
        /// Deletes existing Resident object in SQL database
        /// </summary>
        /// <param name="id">Identification key of Resident to be deleted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(Resident))]
        public IHttpActionResult DeleteResident(long id)
        {
            Resident resident = db.ResidentSet.Find(id);
            if (resident == null)
            {
                return NotFound();
            }

            db.ResidentSet.Remove(resident);
            db.SaveChanges();

            return Ok(resident);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ResidentExists(long id)
        {
            return db.ResidentSet.Count(e => e.PersonalCode == id) > 0;
        }
    }
}