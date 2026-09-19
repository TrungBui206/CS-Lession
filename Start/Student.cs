using System;
using System.Collections.Generic;
using System.Text;

namespace ExampleCAdvance.Entities
{
    public class Student
    {
        private string id;
        private string name;
        private DateTime birthDay;

        [Required]
        public string FullName { get; set; }

        public string Id
        {
            get { return id; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("id cannot be null");
                }
                id = value;
            }
        }

        public Student(string id, string name, DateTime birthDay)
        {
            this.Id = id;
            this.name = name;
            this.birthDay = birthDay;
        }

        public string GetLastName()
        {
            string[] nameParts = FullName.Trim().Split(' ');
            return nameParts[nameParts.Length - 1];
        }

        public string GetFirstName()
        {
            string[] nameParts = FullName.Trim().Split(' ');
            return nameParts[0];
        }
    }
}
