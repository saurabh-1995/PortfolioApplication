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
using CompanyWebApi.Models;

namespace CompanyWebApi.Controllers
{
    public class tblCompanyDetailsController : ApiController
    {
        private DBContext db = new DBContext();

        // GET: api/tblCompanyDetails
        public IQueryable<tblCompanyDetail> GettblCompanyDetails()
        {
            return db.tblCompanyDetails;
        }

        // GET: api/tblCompanyDetails/5
        [ResponseType(typeof(tblCompanyDetail))]
        public IHttpActionResult GettblCompanyDetail(int id)
        {
            tblCompanyDetail tblCompanyDetail = db.tblCompanyDetails.Find(id);
            if (tblCompanyDetail == null)
            {
                return NotFound();
            }

            return Ok(tblCompanyDetail);
        }

        // PUT: api/tblCompanyDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblCompanyDetail(int id, tblCompanyDetail tblCompanyDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblCompanyDetail.CompanyID)
            {
                return BadRequest();
            }

            db.Entry(tblCompanyDetail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblCompanyDetailExists(id))
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

        // POST: api/tblCompanyDetails
        [ResponseType(typeof(tblCompanyDetail))]
        public IHttpActionResult PosttblCompanyDetail(tblCompanyDetail tblCompanyDetail)
        {
           

            db.tblCompanyDetails.Add(tblCompanyDetail);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (tblCompanyDetailExists(tblCompanyDetail.CompanyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tblCompanyDetail.CompanyID }, tblCompanyDetail);
        }

        // DELETE: api/tblCompanyDetails/5
        [ResponseType(typeof(tblCompanyDetail))]
        public IHttpActionResult DeletetblCompanyDetail(int id)
        {
            tblCompanyDetail tblCompanyDetail = db.tblCompanyDetails.Find(id);
            if (tblCompanyDetail == null)
            {
                return NotFound();
            }

            db.tblCompanyDetails.Remove(tblCompanyDetail);
            db.SaveChanges();

            return Ok(tblCompanyDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblCompanyDetailExists(int id)
        {
            return db.tblCompanyDetails.Count(e => e.CompanyID == id) > 0;
        }
    }
}