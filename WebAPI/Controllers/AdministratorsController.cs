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
            return db.AdministratorSet.ToList().Select(x => x.ToContract()).ToList();
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

        [ResponseType(typeof(AdministratorContract))]
        public IHttpActionResult GetAdministrator(string user)
        {
            AdministratorContract administrator = db.AdministratorSet.FirstOrDefault(x=> x.Username == user).ToContract();
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
            Administrator temp = administrator.ToInternal();
            db.DormitorySet.Attach(temp.Dormitory);
            db.AdministratorSet.Add(temp);

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

        public IHttpActionResult Login(string login, string password)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var obj = db.AdministratorSet.Where(a => a.Username.Equals(login) && a.Password.Equals(password)).FirstOrDefault();
                if(obj != null)
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

        // DELETE: api/Administrators/5
        [ResponseType(typeof(AdministratorContract))]
        public IHttpActionResult DeleteAdministrator(long id)
        {
            Administrator temp = db.AdministratorSet.Find(id);
            AdministratorContract administrator = temp.ToContract();
            if (administrator == null)
            {
                return NotFound();
            }

            db.AdministratorSet.Remove(temp);
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