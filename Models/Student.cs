using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Student
    {
        [Key]
        public long SID { get; set; }
        public string SName { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }

        public ICollection<Subject> StudentSubjects {get; set;} = new List<Subject>();

        public ICollection<Sports> StudentSports {get; set;} = new List<Sports>();

    }
}