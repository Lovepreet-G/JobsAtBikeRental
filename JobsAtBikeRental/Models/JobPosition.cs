using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobsAtBikeRental.Models
{
    public class JobPosition
    {
        [Key]
        public int JobPositionId { get; set; }
        public string JobTitle { get; set; }

        public string JobLocation { get; set; }

        public ICollection<ApplicantHistory> ApplicantHistories { get; set; }
    }
    public class JobPositionDTO
    {
        public int JobPositionId { get; set; }
        public string JobTitle { get; set; }
        public string JobLocation { get; set; }


    }
}