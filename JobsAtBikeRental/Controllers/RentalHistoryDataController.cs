using JobsAtBikeRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobsAtBikeRental.Controllers
{
    public class RentalHistoryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Retrieves a list of all rental histories in the system.
        /// </summary>
        /// <returns>
        /// A list of rentalHistoryDto objects representing the rental histories.
        /// </returns>
        /// <example>
        /// GET: api/RentalData/ListRentals
        /// </example>
        [HttpGet]
        [Route("api/RentalData/ListRentals")]
        public List<rentalHistoryDto> ListRentals()
        {
            List<RentalHistory> rentals = db.rentalHistories.ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                RentalId = r.RentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.Customer.customerId,
                customerName = r.Customer.customerName,

            }));

            return rentalHistoryDtos;
        }

        /// <summary>
        /// Retrieves a list of rental histories for a specific bike.
        /// </summary>
        /// <param name="id">The ID of the bike to filter rental histories.</param>
        /// <returns>
        /// A list of rentalHistoryDto objects representing the rental histories for the bike with the specified ID.
        /// </returns>
        /// <example>
        /// GET: api/RentalData/GetRentalsByBikeId/1
        /// </example>
        [HttpGet]
        [Route("api/RentalData/GetRentalsByBikeId/{id}")]
        public List<rentalHistoryDto> GetRentalsByBikeId(int id)
        {
            List<RentalHistory> rentals = db.rentalHistories.Where(r => r.BikeId == id).ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                RentalId = r.RentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.Customer.customerId,
                customerName = r.Customer.customerName,

            }));

            return rentalHistoryDtos;
        }

        /// <summary>
        /// Retrieves a list of rental histories for a specific customer.
        /// </summary>
        /// <param name="id">The ID of the customer to filter rental histories.</param>
        /// <returns>A list of rentalHistoryDto objects representing the rental histories for the customer with the specified ID.</returns>
        /// <example>
        /// GET: api/RentalData/GetRentalsByCustomerId/1
        /// </example>
        [HttpGet]
        [Route("api/RentalData/GetRentalsByCustomerId/{id}")]
        public List<rentalHistoryDto> GetRentalsByCustomerId(int id)
        {
            List<RentalHistory> rentals = db.rentalHistories.Where(r => r.customerId == id).ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                RentalId = r.RentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.Customer.customerId,
                customerName = r.Customer.customerName,

            }));

            return rentalHistoryDtos;
        }
        /// <summary>
        /// Retrieves a list of rental histories for a specific staff member.
        /// </summary>
        /// <param name="id">The ID of the staff member to filter rental histories.</param>
        /// <returns>
        /// A list of rentalHistoryDto objects representing the rental histories for the staff member with the specified ID.
        /// </returns>
        /// <example>
        /// GET: api/RentalData/GetRentalsByStaffId/1
        /// </example>
        [HttpGet]
        [Route("api/RentalData/GetRentalsByStaffId/{id}")]
        public List<rentalHistoryDto> GetRentalsByStaffId(int id)
        {
            List<RentalHistory> rentals = db.rentalHistories.Where(r => r.StaffId == id).ToList();
            List<rentalHistoryDto> rentalHistoryDtos = new List<rentalHistoryDto>();

            rentals.ForEach(r => rentalHistoryDtos.Add(new rentalHistoryDto()
            {
                RentalId = r.RentalId,
                from = r.from,
                to = r.to,
                BikeId = r.bike.BikeId,
                BikeBrand = r.bike.BikeBrand,
                BikeModel = r.bike.BikeModel,
                BikeRate = r.bike.BikeRate,
                customerId = r.Customer.customerId,
                customerName = r.Customer.customerName,
                StaffId = r.StaffId,
                StaffName = r.Staff.StaffName,
                StaffEmail = r.Staff.StaffEmail
            }));

            return rentalHistoryDtos;
        }

    }
}
