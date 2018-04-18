using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataModels;
using WebAPI.Extensions;
using AdministratorContract = DataContract.Objects.Administrator;

namespace WebAPI.Controllers
{
    public class AdministratorsController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Administrators
        public List<AdministratorContract> GetAdministratorSet()
        {
            return db.AdministratorSet.Select(a => a.ToContract()).ToList();
        }

        // GET: api/Administrators/5
        [ResponseType(typeof(AdministratorContract))]
        public IHttpActionResult GetAdministrator(long id)
        {
            AdministratorContract administrator = db.AdministratorSet.Find(id).ToContract();
            if (administrator == null)
            {
                return NotFound();
            }

            return Ok(administrator);
        }

        // PUT: api/Administrators/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAdministrator(long id, AdministratorContract administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != administrator.PersonalCode)
            {
                return BadRequest();
            }

            db.Entry(administrator.ToInternal()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministratorExists(id))
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

        // POST: api/Administrators
        [ResponseType(typeof(AdministratorContract))]
        public IHttpActionResult PostAdministrator(AdministratorContract administrator)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AdministratorSet.Add(administrator.ToInternal());

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AdministratorExists(administrator.PersonalCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = administrator.PersonalCode }, administrator);
        }

        // DELETE: api/Administrators/5
        [ResponseType(typeof(AdministratorContract))]
        public IHttpActionResult DeleteAdministrator(long id)
        {
            AdministratorContract administrator = db.AdministratorSet.Find(id).ToContract();
            if (administrator == null)
            {
                return NotFound();
            }

            db.AdministratorSet.Remove(administrator.ToInternal());
            db.SaveChanges();

            return Ok(administrator);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AdministratorExists(long id)
        {
            return db.AdministratorSet.Any(e => e.PersonalCode == id);
        }
    }
}