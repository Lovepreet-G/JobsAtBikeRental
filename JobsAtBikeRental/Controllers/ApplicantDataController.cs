using JobsAtBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Diagnostics;

namespace JobsAtBikeRental.Controllers
{
    public class ApplicantDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all Applicants in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Applicants in the database
        /// </returns>
        /// <example>
        /// GET: api/ApplicantData/ListApplicant
        /// </example>
        [HttpGet]
        [Route("api/ApplicantData/ListApplicants")]
        [ResponseType(typeof(ApplicantDTO))]
        public IHttpActionResult ListApplicants()
        {
            List<Applicant> applicants = db.Applicants.ToList();
            List<ApplicantDTO> applicantDTOs = new List<ApplicantDTO>();

            applicants.ForEach(a => applicantDTOs.Add(new ApplicantDTO()
            {
                ApplicantId = a.ApplicantId,
                ApplicantName = a.ApplicantName,
                ApplicantEmail = a.ApplicantEmail,
                ApplicantPortfolioUrl = a.ApplicantPortfolioUrl

            }));

            return Ok(applicantDTOs);
        }

        /// <summary>
        /// Returns an Applicant info in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Applicant in the system matching up to the Applicant ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Applicant</param>
        /// <example>
        /// GET: api/ApplicantData/FindApplicant/5
        /// </example>
        [ResponseType(typeof(ApplicantDTO))]
        [Route("api/ApplicantData/FindApplicant/{id}")]
        [HttpGet]
        public IHttpActionResult FindApplicant(int id)
        {
            Applicant a = db.Applicants.Find(id);
            ApplicantDTO applicantDtos = new ApplicantDTO()
            {
                ApplicantId = a.ApplicantId,
                ApplicantName = a.ApplicantName,
                ApplicantEmail = a.ApplicantEmail,
                ApplicantPortfolioUrl = a.ApplicantPortfolioUrl
            };
            if (a == null)
            {
                return NotFound();
            }

            return Ok(applicantDtos);
        }
        /// <summary>
        /// Returns all Applicants in the system associated with a particular position.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Applicants applied on particular position
        /// </returns>
        /// <param name="id">Job Position Primary Key</param>
        /// <example>
        /// GET: api/ApplicantData/ListApplicantsForJobPositionn/1
        /// </example>
        [HttpGet]
        [Route("api/ApplicantData/ListApplicantsForJobPosition/{id}")]
        [ResponseType(typeof(ApplicantDTO))]
        public IHttpActionResult ListApplicantsForJobPosition(int id)
        {

            List<Applicant> applicants = db.Applicants.Where(
                a => a.ApplicantHistories.Any(
                    j => j.JobPositionId == id)
                ).ToList();
            List<ApplicantDTO> applicantDTOs = new List<ApplicantDTO>();

            applicants.ForEach(a => applicantDTOs.Add(new ApplicantDTO()
            {
                ApplicantId = a.ApplicantId,
                ApplicantName = a.ApplicantName,
                ApplicantEmail = a.ApplicantEmail,
                ApplicantPortfolioUrl = a.ApplicantPortfolioUrl
            }));

            return Ok(applicantDTOs);
        }
        /// <summary>
        /// Updates a particular Applicant in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Applicant ID primary key</param>
        /// <param name="Diego">JSON FORM DATA of an applicant</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/ApplicantData/UpdateApplicant/1
        /// FORM DATA: applicant JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/ApplicantData/UpdateApplicant/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateApplicant(int id, Applicant a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != a.ApplicantId)
            {

                return BadRequest();
            }

            db.Entry(a).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicantExists(id))
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
        /// <summary>
        /// Adds an Applicant to the system
        /// </summary>
        /// <param name="applicant">JSON FORM DATA of an Applicant</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Applicant ID, Applicant Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/ApplicantData/AddApplicant
        /// FORM DATA: applicant JSON Object
        /// </example>
        [ResponseType(typeof(Applicant))]
        [HttpPost]
        public IHttpActionResult AddApplicant(Applicant a)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Applicants.Add(a);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = a.ApplicantId }, a);
        }
        /// <summary>
        /// Deletes an Applicant from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Applicant</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/ApplicantData/DeleteApplicant/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Applicant))]
        [Route("api/ApplicantData/DeleteApplicant/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteApplicant(int id)
        {
            Applicant a = db.Applicants.Find(id);
            if (a == null)
            {
                return NotFound();
            }

            db.Applicants.Remove(a);
            db.SaveChanges();

            return Ok();
        }
        private bool ApplicantExists(int id)
        {
            return db.Applicants.Count(c => c.ApplicantId == id) > 0;
        }
    }
}
