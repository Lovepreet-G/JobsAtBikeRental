using JobsAtBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace JobsAtBikeRental.Controllers
{
    public class BikeDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Returns List of all bike 
        /// </summary>
        /// <returns>
        /// Content : List of all bike
        /// </returns>
        /// <example>
        /// GET: api/BikeData/ListBikes
        /// </example>
        [HttpGet]
        [Route("api/BikeData/ListBikes")]
        public List<bikeDto> ListBikes()
        {
            List<bike> bikes = db.bikes.ToList();
            List<bikeDto> bikeDtos = new List<bikeDto>();

            bikes.ForEach(b => bikeDtos.Add(new bikeDto()
            {
                BikeId = b.BikeId,
                BikeBrand = b.BikeBrand,
                BikeModel = b.BikeModel,
                BikeRate = b.BikeRate,
            }));

            return bikeDtos;
        }

        /// <summary>
        /// Returns data of a bike
        /// </summary>
        /// <param name="id">Primary key of Bike</param>
        /// <returns>
        /// Content : A Bike in the system matching up to the Bike ID primary key
        /// </returns>
        /// <example>
        /// GET: api/BikeData/FindBike/1
        /// </example>
        [ResponseType(typeof(bike))]
        [HttpGet]
        [Route("api/BikeData/FindBike/{id}")]
        public IHttpActionResult FindBike(int id)
        {
            bike bike = db.bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }
            bikeDto bikeDto = new bikeDto()
            {
                BikeId = bike.BikeId,
                BikeBrand = bike.BikeBrand,
                BikeModel = bike.BikeModel,
                BikeRate = bike.BikeRate,
            };
            return Ok(bikeDto);
        }

        /// <summary>
        /// Updates the information of a bike in the system.
        /// </summary>
        /// <param name="id">The primary key of the bike to be updated.</param>
        /// <param name="bike">The updated bike object containing the new information.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/BikeData/UpdateBike/1
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/BikeData/UpdateBike/{id}")]
        public IHttpActionResult UpdateBike(int id, bike bike)
        {

            if (!ModelState.IsValid)
            {
                Debug.WriteLine("Model State is invalid");
                return BadRequest(ModelState);
            }

            if (id != bike.BikeId)
            {
                Debug.WriteLine("ID mismatch");
                Debug.WriteLine("GET parameter" + id);
                Debug.WriteLine("POST parameter" + bike.BikeId);
                Debug.WriteLine("POST parameter" + bike.BikeBrand);
                Debug.WriteLine("POST parameter " + bike.BikeModel);
                Debug.WriteLine("POST parameter " + bike.BikeRate);
                return BadRequest();
            }

            db.Entry(bike).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!bikeExists(id))
                {
                    Debug.WriteLine("bike not found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Debug.WriteLine("None of the conditions triggered");
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds a new bike to the system.
        /// </summary>
        /// <param name="bike">The bike object to be added.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/BikeData/Addbike
        /// </example>
        [ResponseType(typeof(bike))]
        [HttpPost]
        public IHttpActionResult AddBike(bike bike)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.bikes.Add(bike);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = bike.BikeId }, bike);
        }

        /// <summary>
        /// Deletes a bike from the system.
        /// </summary>
        /// <param name="id">The primary key of the bike to be deleted.</param>
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/BikeData/DeleteBike/5
        /// </example>
        
        [ResponseType(typeof(bike))]
        [Route("api/BikeData/DeleteBike/{id}")]
        [HttpPost]
        public IHttpActionResult DeleteBike(int id)
        {
            bike bike = db.bikes.Find(id);
            if (bike == null)
            {
                return NotFound();
            }

            db.bikes.Remove(bike);
            db.SaveChanges();

            return Ok();
        }
        /// <summary>
        /// Checks if a bike exists in the system based on its primary key.
        /// </summary>
        /// <param name="id">The primary key of the bike.</param>
        /// <returns>
        /// True if the bike exists, otherwise false.
        /// </returns>
        private bool bikeExists(int id)
        {
            return db.bikes.Count(e => e.BikeId == id) > 0;
        }


    }
}
