using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentApi.Models
{
    public class Sports
    {
        [Key]
        public int SportsID { get; set; }
        public string SportsName { get; set; }
        [ForeignKey("StudID")]
        public Student student { get; set; }
        public long StudID { get; set; }
    }
}