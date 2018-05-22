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
using GuardContract = DataContract.Objects.Guard;

namespace WebAPI.Controllers
{
    public class GuardsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Guards
        /// <summary>
        /// Gets list of Guards from database
        /// </summary>
        /// <returns>List of Guard objects</returns>
        public List<GuardContract> GetGuardSet()
        {
            return db.GuardSet.ToList().Select(x => x.ToContract()).ToList();
        }

        // GET: api/Guards/5
        /// <summary>
        /// Gets guard by its identity key
        /// </summary>
        /// <param name="id">Identity key</param>
        /// <returns>Guard object</returns>
        [ResponseType(typeof(GuardContract))]
        public IHttpActionResult GetGuard(long id)
        {
            GuardContract guard = db.GuardSet.Find(id).ToContract();
            if (guard == null)
            {
                return NotFound();
            }

            return Ok(guard);
        }

        /// <summary>
        /// Gets guard by its system login data
        /// </summary>
        /// <param name="user">System login username</param>
        /// <returns>Guard object</returns>
        [ResponseType(typeof(GuardContract))]
        public IHttpActionResult GetGuard(string user)
        {
            GuardContract guard = db.GuardSet.FirstOrDefault(x => x.Username == user).ToContract();
            if (guard == null)
            {
                return NotFound();
            }

            return Ok(guard);
        }

        // PUT: api/Guards/5
        /// <summary>
        /// Updates existing Guard
        /// </summary>
        /// <param name="id">Identity key of guard to be updated</param>
        /// <param name="guard">Updated guard data</param>
        /// <returns>Request status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGuard(long id, GuardContract guard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guard.PersonalCode)
            {
                return BadRequest();
            }
            db.Entry(guard.ToInternal()).State = EntityState.Modified;

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
        /// <summary>
        /// Inserts new Guard into database
        /// </summary>
        /// <param name="guard">Guard object to be inserted</param>
        /// <returns>Object that was inserted or request status</returns>
        [ResponseType(typeof(GuardContract))]
        public IHttpActionResult PostGuard(GuardContract guard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Guard temp = guard.ToInternal();
            db.DormitorySet.Attach(temp.Dormitory);
            db.GuardSet.Add(temp);

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
        /// <summary>
        /// Deletes existing guard from database
        /// </summary>
        /// <param name="id">Guard to be deleted identity key</param>
        /// <returns>Request status or Guard object</returns>
        [ResponseType(typeof(GuardContract))]
        public IHttpActionResult DeleteGuard(long id)
        {
            Guard temp = db.GuardSet.Find(id);
            GuardContract guard = temp.ToContract();
            if (guard == null)
            {
                return NotFound();
            }

            db.GuardSet.Remove(temp);
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

        /// <summary>
        /// Web api controlled login into system
        /// </summary>
        /// <param name="login">Username</param>
        /// <param name="password">Raw password</param>
        /// <returns>Request status</returns>
        public IHttpActionResult Login(string login, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                string pass = WebApiConfig.Decryption(password.Replace(' ', '+'));
                var obj = db.GuardSet.AsEnumerable().Where(a => a.Username.Equals(login) && WebApiConfig.Decryption(a.Password).Equals(pass)).FirstOrDefault();
                if (obj != null)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Bad login credentials");
                }
            }
            catch (System.ArgumentNullException)
            {
                return BadRequest("User wasn't found by credentials entered");
            }
        }
    }
}