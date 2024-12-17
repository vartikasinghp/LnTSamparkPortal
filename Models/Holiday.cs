using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace samp.Models
{
    public class Holiday
    {
        [Key]
        public int HolidayId { get; set; } 

        [Required]
        [Column(TypeName = "date")]
        public DateTime HolidayDate { get; set; } 

        [Required]
        [StringLength(100)]
        public string HolidayName { get; set; } 

        [StringLength(255)]
        public string Location { get; set; } 
    }
}
