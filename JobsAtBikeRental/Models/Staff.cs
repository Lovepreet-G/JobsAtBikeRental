using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JobsAtBikeRental.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffEmail { get; set; }

        [ForeignKey("ApplicantHistory")]
        public int ApplicantHistoryId { get; set; }
        public virtual ApplicantHistory ApplicantHistory { get; set; }

    }
    public class StaffDTO
    {
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string StaffEmail { get; set; }


        public int ApplicantHistoryId { get; set; }
        public string Status { get; set; }

    }
}