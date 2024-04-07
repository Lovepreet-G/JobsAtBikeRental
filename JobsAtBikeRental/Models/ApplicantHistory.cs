using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsAtBikeRental.Models
{
    public class ApplicantHistory
    {
        [Key]
        public int ApplicantHistoryId { get; set; }

        [ForeignKey("Applicant")]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }

        [ForeignKey("JobPosition")]
        public int JobPositionId { get; set; }
        public virtual JobPosition JobPosition { get; set; }


        public string Status { get; set; }
    }
    public class ApplicantHistoryDTO
    {
        public int ApplicantHistoryId { get; set; }
        public string Status { get; set; }


        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantPortfolioUrl { get; set; }


        public int JobPositionId { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }



    }
}