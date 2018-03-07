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
    /// All possible interactions within Visits API
    /// </summary>
    public class VisitsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Visits
        /// <summary>
        /// Returns JSON serialized array of Visit objects
        /// </summary>
        /// <returns>List of Visits</returns>
        public List<Visit> GetVisitSet()
        {
            return db.VisitSet.ToList();
        }

        // GET: api/Visits/5
        /// <summary>
        /// Returns JSON seriliazed Visit object determined by identification key
        /// </summary>
        /// <param name="id">Visit identifaction key</param>
        /// <returns>Http request result</returns>
        [ResponseType(typeof(Visit))]
        public IHttpActionResult GetVisit(int id)
        {
            Visit visit = db.VisitSet.Find(id);
            if (visit == null)
            {
                return NotFound();
            }

            return Ok(visit);
        }

        // PUT: api/Visits/5
        /// <summary>
        /// Updates particular Visit object in SQL database
        /// </summary>
        /// <param name="id">Visit to be updated identifaction key</param>
        /// <param name="visit">Updated Visit object which will replace existing Visit object in SQL database</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVisit(int id, Visit visit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != visit.ID)
            {
                return BadRequest();
            }

            db.Entry(visit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VisitExists(id))
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

        // POST: api/Visits
        /// <summary>
        /// Inserts new Visit object into SQL database
        /// </summary>
        /// <param name="visit">Visit to be inserted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(Visit))]
        public IHttpActionResult PostVisit(Visit visit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VisitSet.Add(visit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = visit.ID }, visit);
        }

        // DELETE: api/Visits/5
        /// <summary>
        /// Deletes existing Visit object in SQL database
        /// </summary>
        /// <param name="id">Identification key of Visit to be deleted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(Visit))]
        public IHttpActionResult DeleteVisit(int id)
        {
            Visit visit = db.VisitSet.Find(id);
            if (visit == null)
            {
                return NotFound();
            }

            db.VisitSet.Remove(visit);
            db.SaveChanges();

            return Ok(visit);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VisitExists(int id)
        {
            return db.VisitSet.Count(e => e.ID == id) > 0;
        }
    }
}