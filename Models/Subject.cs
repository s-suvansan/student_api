using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using StudentApi.Models;

namespace StudentApi
{
    public class Subject
    {
        [Key]
        public long SubjectID { get; set; }
        public string  SubjectTitle { get; set; }
        
        [ForeignKey("StudId")]
        public Student student { get; set; }

        public long StudId { get; set;}
    }
}