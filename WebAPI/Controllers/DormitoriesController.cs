using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI.DataModels;
using WebAPI.Extensions;
using DormitoryContract = DataContract.Objects.Dormitory;

namespace WebAPI.Controllers
{
    /// <summary>
    /// All possible interactions within Dormitories API
    /// </summary>
    public class DormitoriesController : ApiController
    {
        private MainDbModelContainer1 db = new MainDbModelContainer1();

        // GET: api/Dormitories
        /// <summary>
        /// Returns JSON serialized array of Dormitory objects
        /// </summary>
        /// <returns>List of Dormitories</returns>
        public List<DormitoryContract> GetDormitorySet()
        {
            return db.DormitorySet.Select(d => d.ToContract()).ToList();
        }

        // GET: api/Dormitories/5
        /// <summary>
        /// Returns JSON seriliazed Dormitory object determined by identification key
        /// </summary>
        /// <param name="id">Visit identifaction key</param>
        /// <returns>Http request result</returns>
        [ResponseType(typeof(DormitoryContract))]
        public IHttpActionResult GetDormitory(int id)
        {
            DormitoryContract dormitory = db.DormitorySet.Find(id).ToContract();
            if (dormitory == null)
            {
                return NotFound();
            }

            return Ok(dormitory);
        }

        // PUT: api/Dormitories/5
        /// <summary>
        /// Updates particular Dormitory object in SQL database
        /// </summary>
        /// <param name="id">Dormitory to be updated identifaction key</param>
        /// <param name="dormitory">Updated Dormitory object which will replace existing Dormitory object in SQL database</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDormitory(int id, DormitoryContract dormitory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dormitory.ID)
            {
                return BadRequest();
            }

            db.Entry(dormitory.ToInternal()).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DormitoryExists(id))
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

        // POST: api/Dormitories
        /// <summary>
        /// Inserts new Dormitory object into SQL database
        /// </summary>
        /// <param name="dormitory">Dormitory to be inserted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(DormitoryContract))]
        public IHttpActionResult PostDormitory(DormitoryContract dormitory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DormitorySet.Add(dormitory.ToInternal());
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dormitory.ID }, dormitory);
        }

        // DELETE: api/Dormitories/5
        /// <summary>
        /// Deletes existing Dormitory object in SQL database
        /// </summary>
        /// <param name="id">Identification key of Dormitory to be deleted</param>
        /// <returns>Http request status</returns>
        [ResponseType(typeof(DormitoryContract))]
        public IHttpActionResult DeleteDormitory(int id)
        {
            DormitoryContract dormitory = db.DormitorySet.Find(id).ToContract();
            if (dormitory == null)
            {
                return NotFound();
            }

            db.DormitorySet.Remove(dormitory.ToInternal());
            db.SaveChanges();

            return Ok(dormitory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DormitoryExists(int id)
        {
            return db.DormitorySet.Count(e => e.ID == id) > 0;
        }
    }
}