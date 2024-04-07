using JobsAtBikeRental.Migrations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsAtBikeRental.Models
{
    public class RentalHistory
    {
        [Key]
        public int RentalId { get; set; }
        [ForeignKey("bike")]
        public int BikeId { get; set; }
        public virtual bike bike { get; set; }

        [ForeignKey("Customer")]
        public int customerId { get; set; }
        public virtual Customer Customer { get; set; }

        [ForeignKey("Staff")]
        public int StaffId { get; set; }
        public virtual Staff Staff { get; set; }

        public DateTime from { get; set; }

        public DateTime to { get; set; }
    }
    public class rentalHistoryDto
    {
        public int RentalId { get; set; }
        public string customerName { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
        public int BikeId { get; set; }
        public string BikeBrand { get; set; }
        public string BikeModel { get; set; }
        public int BikeRate { get; set; }
        public int customerId { get; set; }

        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffEmail { get; set; }
    }
}