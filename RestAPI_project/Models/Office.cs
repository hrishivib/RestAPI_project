using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RestAPI_project.Models
{
    public class Office
    {
        public int Id { get; set; }

        [Required]
        public string Location { get; set; }

        // Foreign Key
        public int EmployeeId { get; set; }

        // Navigation Property
        public Employee Employee { get; set; }


    }
}