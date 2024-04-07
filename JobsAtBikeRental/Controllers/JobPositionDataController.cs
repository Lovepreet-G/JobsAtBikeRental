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
using JobsAtBikeRental.Migrations;

namespace JobsAtBikeRental.Controllers
{
    public class JobPositionDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns all JobPositions in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all JobPosition in the database
        /// </returns>
        /// <example>
        /// GET: api/JobPositionData/ListJobPositions
        /// </example>
        [HttpGet]
        [Route("api/JobPositionData/ListJobPositions")]
        [ResponseType(typeof(JobPositionDTO))]
        public IHttpActionResult ListJobPositions()
        {
            List<JobPosition> jobPositions = db.JobPositions.ToList();
            List<JobPositionDTO> jobPositionsDTOs = new List<JobPositionDTO>();

            jobPositions.ForEach(j => jobPositionsDTOs.Add(new JobPositionDTO()
            {
                JobPositionId = j.JobPositionId,
                JobTitle = j.JobTitle,
                JobLocation = j.JobLocation
            }));

            return Ok(jobPositionsDTOs);

        }
        /// <summary>
        /// Gathers information about jobApplications related to a particular applicant
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all jobApplications in the database that match to a particular applicant id
        /// </returns>
        /// <param name="id">applicant ID.</param>
        /// <example>
        /// GET: api/jobPositionData/ListJobApplicationsOfApplicant/1
        /// </example>
        [HttpGet]
        [Route("api/jobPositionData/ListJobApplicationsOfApplicant/{id}")]
        [ResponseType(typeof(JobPositionDTO))]
        public IHttpActionResult ListJobApplicationOfUser(int id)
        {
            List<JobPosition> jobPositions = db.JobPositions.Where(
                j => j.ApplicantHistories.Any(
                    u => u.ApplicantId == id
                )).ToList();
            List<JobPositionDTO> jobPositionDTOs = new List<JobPositionDTO>();

            jobPositions.ForEach(j => jobPositionDTOs.Add(new JobPositionDTO()
            {
                JobPositionId = j.JobPositionId,
                JobTitle = j.JobTitle,
                JobLocation = j.JobLocation
            }));

            return Ok(jobPositionDTOs);
        }
        /// <summary>
        /// Returns JobPosition in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An JobPosition in the system matching up to the JobPosition ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the JobPosition</param>
        /// <example>
        /// GET: api/JobPositionData/FindJobPosition/5
        /// </example>
        [ResponseType(typeof(JobPositionDTO))]
        [Route("api/JobPositionData/FindJobPosition/{id}")]
        [HttpGet]
        public IHttpActionResult FindJobPosition(int id)
        {
            JobPosition j = db.JobPositions.Find(id);
            JobPositionDTO jobPositionDTOS = new JobPositionDTO()
            {
                JobPositionId = j.JobPositionId,
                JobTitle = j.JobTitle,
                JobLocation = j.JobLocation
            };
            if (j == null)
            {
                return NotFound();
            }

            return Ok(jobPositionDTOS);
        }
        /// <summary>
        /// Updates a particular JobPosition in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the JobPosition ID primary key</param>
        /// <param name="jobPosition">JSON FORM DATA of an JobPosition</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/JobPositionData/UpdateJobPosition/2
        /// FORM DATA: jobPosition JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/JobPositionData/UpdateJobPosition/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateJobPosition(int id, JobPosition j)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != j.JobPositionId)
            {

                return BadRequest();
            }

            db.Entry(j).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobPositionExists(id))
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
        /// Adds an JobPosition to the system
        /// </summary>
        /// <param name="jobPosition">JSON FORM DATA of an JobPosition</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: JobPosition ID, JobPosition Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/JobPositionData/AddJobPosition
        /// FORM DATA: jobPosition JSON Object
        /// </example>
        [ResponseType(typeof(JobPosition))]
        [Route("api/JobPositionData/AddJobPosition")]
        [HttpPost]
        public IHttpActionResult AddJobPosition(JobPosition j)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.JobPositions.Add(j);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = j.JobPositionId }, j);
        }
        /// <summary>
        /// Deletes an JobPosition from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the JobPosition</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/JobPositionData/DeleteJobPosition/3
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(JobPosition))]
        [Route("api/JobPositionData/DeleteJobPosition/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteJobPosition(int id)
        {
            JobPosition j = db.JobPositions.Find(id);
            if (j == null)
            {
                return NotFound();
            }

            db.JobPositions.Remove(j);
            db.SaveChanges();

            return Ok();
        }
        private bool JobPositionExists(int id)
        {
            return db.JobPositions.Count(e => e.JobPositionId == id) > 0;
        }
    }
}
