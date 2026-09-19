using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ExampleCAdvance.Entities
{
    public class Student
    {
        [Required(ErrorMessage = "StudentID is required")]
        public string StuID { get; set; }

        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        public string Name { get; set; }

        [Range(0, 10, ErrorMessage = "MidPoint must be between 0 and 10")]
        public double MidPoint { get; set; }

        [Range(0, 10, ErrorMessage = "FinalPoint must be between 0 and 10")]
        public double FinalPoint { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

    }
}