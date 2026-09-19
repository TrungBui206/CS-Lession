using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ExampleCAdvance.Entities
{
    public class Subject
    {
        [Required(ErrorMessage = "SubID is required")]
        public string SubID { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Name { get; set; }

        [Range(1, 9, ErrorMessage = "Semester must be between 1 and 9")]
        public int Semester { get; set; }

        [Required(ErrorMessage = "Teacher name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Teacher name must be between 2 and 50 characters")]
        public string Teacher { get; set; }
    }
    
}