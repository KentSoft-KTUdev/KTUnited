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
    public class GuardsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Guards
        public List<Guard> GetGuardSet()
        {
            return db.GuardSet.ToList();
        }

        // GET: api/Guards/5
        [ResponseType(typeof(Guard))]
        public IHttpActionResult GetGuard(long id)
        {
            Guard guard = db.GuardSet.Find(id);
            if (guard == null)
            {
                return NotFound();
            }

            return Ok(guard);
        }

        // PUT: api/Guards/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuard(long id, Guard guard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guard.PersonalCode)
            {
                return BadRequest();
            }

            db.Entry(guard).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuardExists(id))
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

        // POST: api/Guards
        [ResponseType(typeof(Guard))]
        public IHttpActionResult PostGuard(Guard guard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.GuardSet.Add(guard);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (GuardExists(guard.PersonalCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = guard.PersonalCode }, guard);
        }

        // DELETE: api/Guards/5
        [ResponseType(typeof(Guard))]
        public IHttpActionResult DeleteGuard(long id)
        {
            Guard guard = db.GuardSet.Find(id);
            if (guard == null)
            {
                return NotFound();
            }

            db.GuardSet.Remove(guard);
            db.SaveChanges();

            return Ok(guard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuardExists(long id)
        {
            return db.GuardSet.Count(e => e.PersonalCode == id) > 0;
        }
    }
}