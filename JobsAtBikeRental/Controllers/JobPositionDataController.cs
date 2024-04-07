using JobsAtBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

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
    }
}
