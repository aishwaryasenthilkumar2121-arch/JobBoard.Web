using System.ComponentModel.DataAnnotations;

namespace JobBoard.Web.Models
{
    public class Job
    {
        public int JobId { get; set; }

        [Required]
        public string JobTitle { get; set; }

        public string CompanyName { get; set; }

        public string SkillsRequired { get; set; }

        public string Description { get; set; }

        public decimal Salary { get; set; }
    }
}