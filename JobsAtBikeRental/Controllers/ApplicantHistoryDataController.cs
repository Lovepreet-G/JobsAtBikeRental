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
    public class ApplicantHistoryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all Histories in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Histories in the database
        /// </returns>
        /// <example>
        /// GET: api/HistoryData/ListHistories
        /// </example>
        [HttpGet]
        [Route("api/HistoryData/ListHistories")]
        [ResponseType(typeof(ApplicantHistoryDTO))]
        public IHttpActionResult ListHistories()
        {
            List<ApplicantHistory> ah = db.ApplicantHistories.ToList();
            List<ApplicantHistoryDTO> ahDTOs = new List<ApplicantHistoryDTO>();

            ah.ForEach(u => ahDTOs.Add(new ApplicantHistoryDTO()
            {
                ApplicantHistoryId = u.ApplicantHistoryId,
                Status = u.Status

            }));

            return Ok(ahDTOs);
        }
        /// <summary>
        /// Returns all histories in the system associated with a particular Position.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all histories for an particular Position
        /// </returns>
        /// <param name="id">Job Position Primary Key</param>
        /// <example>
        /// GET: api/HistoryData/ListHistoriesForPosition/1
        /// </example>
        [HttpGet]
        [Route("api/HistoryData/ListHistoriesForPosition/{id}")]
        [ResponseType(typeof(ApplicantHistoryDTO))]
        public IHttpActionResult ListHistoriesForPosition(int id)
        {

            List<ApplicantHistory> ah = db.ApplicantHistories.Where(
                    j => j.JobPositionId == id).ToList();
            List<ApplicantHistoryDTO> ahDTOS = new List<ApplicantHistoryDTO>();

            ah.ForEach(u => ahDTOS.Add(new ApplicantHistoryDTO()
            {
                ApplicantHistoryId = u.ApplicantHistoryId,
                Status = u.Status,
                JobPositionId = u.JobPosition.JobPositionId,
                JobTitle = u.JobPosition.JobTitle,
                JobLocation = u.JobPosition.JobLocation,
                ApplicantId=u.Applicant.ApplicantId,
                ApplicantName=u.Applicant.ApplicantName,
                ApplicantEmail=u.Applicant.ApplicantEmail
            }));

            return Ok(ahDTOS);
        }
        /// <summary>
        /// Returns all histories in the system associated with a particular Applicant.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all histories if an particular Applicant
        /// </returns>
        /// <param name="id">Applicant Primary Key</param>
        /// <example>
        /// GET: api/HistoryData/ListHistoriesForApplicant/1
        /// </example>
        [HttpGet]
        [Route("api/HistoryData/ListHistoriesForApplicant/{id}")]
        [ResponseType(typeof(ApplicantHistoryDTO))]
        public IHttpActionResult ListHistoriesForApplicant(int id)
        {

            List<ApplicantHistory> ah = db.ApplicantHistories.Where(
                    j => j.ApplicantId == id).ToList();
            List<ApplicantHistoryDTO> ahDTOS = new List<ApplicantHistoryDTO>();

            ah.ForEach(u => ahDTOS.Add(new ApplicantHistoryDTO()
            {
                ApplicantHistoryId = u.ApplicantHistoryId,
                Status = u.Status,
                JobPositionId = u.JobPosition.JobPositionId,
                JobTitle = u.JobPosition.JobTitle,
                JobLocation = u.JobPosition.JobLocation,
                ApplicantId = u.Applicant.ApplicantId,
                ApplicantName = u.Applicant.ApplicantName,
                ApplicantEmail = u.Applicant.ApplicantEmail
            }));

            return Ok(ahDTOS);
        }
    }
}
