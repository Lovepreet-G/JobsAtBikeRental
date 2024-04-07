using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsAtBikeRental.Models
{
    public class Applicant
    {
        [Key]
        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantPortfolioUrl { get; set; }

        public ICollection<ApplicantHistory> ApplicantHistories { get; set; }

    }
    public class ApplicantDTO
    {
        public int ApplicantId { get; set; }
        public string ApplicantName { get; set; }
        public string ApplicantEmail { get; set; }
        public string ApplicantPortfolioUrl { get; set; }


    }
}