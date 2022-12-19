using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RestAPIDemo.Data;
using RestAPI_project.Models;
using RestAPIDemo.Models;
using Caching;

namespace RestAPIDemo.Controllers
{
    public class OfficesController : ApiController
    {
        private RestAPIDemoContext db = new RestAPIDemoContext();

        // GET: api/Offices
        [CacheFilter(Duration = 300)]
        public IQueryable<OfficeDTO> GetOffices()
        {
            var offices = from x in db.Offices
                          select new OfficeDTO()
                          {
                              Id = x.Id, // 'Id' is from OfficeDTO and 'x.Id' is from Office  
                              EmployeeName = x.Employee.Name // 'EmployeeName' is from OfficeDTO and 'x.Employee.Name' is from 'Employee' Class/Model 
                          };
            return offices;
        }

        // GET: api/Offices/5
        [ResponseType(typeof(OfficeDetailDTO))]
        public async Task<IHttpActionResult> GetOffice(int id)
        {
            var office = await db.Offices.Include(x => x.Employee).Select(x =>
              new OfficeDetailDTO()
              {
                  Id = x.Id,
                  Location = x.Location,
                  EmployeeName = x.Employee.Name
              }).SingleOrDefaultAsync(b => b.Id == id);

            if (office == null)
            {
                return NotFound();
            }

            return Ok(office);
        }

        // PUT: api/Offices/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutOffice(int id, Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != office.Id)
            {
                return BadRequest();
            }

            db.Entry(office).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfficeExists(id))
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

        // POST: api/Offices
        [ResponseType(typeof(Office))]
        public async Task<IHttpActionResult> PostOffice(Office office)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Offices.Add(office);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = office.Id }, office);
        }

        // DELETE: api/Offices/5
        [ResponseType(typeof(Office))]
        public async Task<IHttpActionResult> DeleteOffice(int id)
        {
            Office office = await db.Offices.FindAsync(id);
            if (office == null)
            {
                return NotFound();
            }

            db.Offices.Remove(office);
            await db.SaveChangesAsync();

            return Ok(office);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool OfficeExists(int id)
        {
            return db.Offices.Count(e => e.Id == id) > 0;
        }
    }
}