using JobsAtBikeRental.Migrations;
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

namespace JobsAtBikeRental.Controllers
{
    public class StaffDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        /// <summary>
        /// Returns all Staffs in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Staffs in the database
        /// </returns>
        /// <example>
        /// GET: api/StaffData/ListStaffs
        /// </example>
        [HttpGet]
        [Route("api/StaffData/ListStaffs")]
        [ResponseType(typeof(StaffDTO))]
        public IHttpActionResult ListUsers()
        {
            List<Staff> staffs = db.Staffs.ToList();
            List<StaffDTO> staffDTOs = new List<StaffDTO>();

            staffs.ForEach(u => staffDTOs.Add(new StaffDTO()
            {
                StaffId = u.StaffId,
                StaffName = u.StaffName,
                StaffEmail = u.StaffEmail,
                ApplicantHistoryId = u.ApplicantHistoryId,
                Status = u.ApplicantHistory.Status

            }));

            return Ok(staffDTOs);
        }
        /// <summary>
        /// Returns all Staffs in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: An Staff in the system matching up to the Staff ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Staff</param>
        /// <example>
        /// GET: api/StaffData/FindStaff/1
        /// </example>
        [ResponseType(typeof(StaffDTO))]
        [Route("api/StaffData/FindStaff/{id}")]
        [HttpGet]
        public IHttpActionResult FindUser(int id)
        {
            Staff u = db.Staffs.Find(id);
            StaffDTO StaffDTOs = new StaffDTO()
            {
                StaffId = u.StaffId,
                StaffName = u.StaffName,
                StaffEmail = u.StaffEmail,
                ApplicantHistoryId = u.ApplicantHistoryId,
                Status = u.ApplicantHistory.Status
            };
            if (u == null)
            {
                return NotFound();
            }

            return Ok(StaffDTOs);
        }
        /// <summary>
        /// Updates a particular Staff in the system with POST Data input
        /// </summary>
        /// <param name="id">Represents the Staff ID primary key</param>
        /// <param name="Staff">JSON FORM DATA of an staff</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/StaffData/UpdateStaff/1
        /// FORM DATA: staff JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [Route("api/StaffData/UpdateStaff/{id}")]
        [HttpPost]
        public IHttpActionResult UpdateStaff(int id, Staff s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != s.StaffId)
            {

                return BadRequest();
            }

            db.Entry(s).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(id))
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
        /// Adds an Staff to the system
        /// </summary>
        /// <param name="staff">JSON FORM DATA of an Staff</param>
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Staff ID, Staff Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/StaffData/AddStaff
        /// FORM DATA: staff JSON Object
        /// </example>
        [ResponseType(typeof(Staff))]
        [HttpPost]
        public IHttpActionResult AddStaff(Staff s)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Staffs.Add(s);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = s.StaffId }, s);
        }
        /// <summary>
        /// Deletes an Staff from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Staff</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/StaffData/DeleteStaff/3
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Staff))]
        [Route("api/StaffData/DeleteStaff/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteUser(int id)
        {
            Staff s = db.Staffs.Find(id);
            if (s == null)
            {
                return NotFound();
            }

            db.Staffs.Remove(s);
            db.SaveChanges();

            return Ok();
        }
        private bool StaffExists(int id)
        {
            return db.Staffs.Count(e => e.StaffId == id) > 0;
        }
    }
}
