using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsAtBikeRental.Models
{
    public class Customer
    {
        [Key]
        public int customerId { get; set; }

        public string customerName { get; set; }

        public string customerAddress { get; set; }

        public int customerPhone { get; set; }
    }

    public class customerDto
    {
        public int customerId { get; set; }

        public string customerName { get; set; }

        public string customerAddress { get; set; }

        public int customerPhone { get; set; }

    }
}